using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyPOI;
namespace ChatApp
{
    public partial class FormHome : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;
        private FormCreateChat formCreateChat;
        private Dictionary<int, formChat> chatsForms = new Dictionary<int, formChat>();
        private ToolStripMenuItem selectedStateItem;
        public FormHome()
        {
            InitializeComponent();
        }

        private void FormHome_MouseDown(object sender, MouseEventArgs e) {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void FormHome_MouseMove(object sender, MouseEventArgs e) {
            if (dragging) {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void FormHome_MouseUp(object sender, MouseEventArgs e) {
            dragging = false;
        }

        private void picBox_CloseIcon_MouseEnter(object sender, EventArgs e) {
            picBox_CloseIcon.BackColor = Color.Brown;
        }

        private void picBox_CloseIcon_MouseLeave(object sender, EventArgs e) {
            picBox_CloseIcon.BackColor = Color.White;
        }

        private void picBox_CloseIcon_MouseClick(object sender, MouseEventArgs e) {
            this.Close();
        }
        //Thread safe callbacks
        //http://stackoverflow.com/questions/10775367/cross-thread-operation-not-valid-control-textbox1-accessed-from-a-thread-othe
        internal void OnPacket(Packet packet)
        {
            if (this.InvokeRequired)
            {
                ClientSession.ReceivePacketCallback d = new ClientSession.ReceivePacketCallback(OnPacket);
                this.Invoke(d, new object[] { packet });
            }
            else
            {
                switch(packet.Type)
                {
                    case PacketType.CreatePublicConversation:
                        {
                            var item = listViewConversacion.Items.Add(new ListViewItem(new string[] {packet.tag["nombre"] as string, " " }));
                            int chatID = (int)packet.tag["id"];
                            chatsForms.Add(chatID, new formChat(chatID, item));
                            item.Tag = chatsForms[chatID];
                            formCreateChat.Close();
                        }
                        break;
                    case PacketType.GetUserConversations:
                        {
                            ClientSession.chats = packet.tag["conversations"] as Dictionary<int, string>;
                            foreach(var c in ClientSession.chats)
                            {
                                var item = listViewConversacion.Items.Add(new ListViewItem(new string[] { c.Value, " " }));
                                chatsForms.Add(c.Key, new formChat(c.Key, item));
                                item.Tag = chatsForms[c.Key];
                            }
                            Packet sendPacket = new Packet(PacketType.SetUserState);
                            sendPacket.tag["user"] = ClientSession.username;
                            sendPacket.tag["state"] = ClientSession.state;
                            ClientSession.Connection.SendPacket(sendPacket);
                        }
                        break;
                    case PacketType.GetChatConversation:
                        {
                            int chatID = (int)packet.tag["chatID"];
                            List<Tuple<string, string>> messages = (List<Tuple<string, string>>)packet.tag["messages"];
                            foreach (var message in messages)
                            {
                                chatsForms[chatID].richTextBoxChat.Text += message.Item2 + ": ";
                                chatsForms[chatID].richTextBoxChat.Text += message.Item1 + "\n";
                            }
                            if(messages.Count>0)
                            {
                                chatsForms[chatID].listItem.SubItems[1].Text = messages.Last().Item2 + ": " + messages.Last().Item1;
                            }
                        }
                        break;
                    case PacketType.TextMessage:
                        {
                            int chatID = (int)packet.tag["chatID"];
                            string textString = packet.tag["sender"] + ": " + packet.tag["text"];
                            chatsForms[chatID].richTextBoxChat.Text += "\n" + textString;
                            chatsForms[chatID].listItem.SubItems[1].Text = textString;
                        }
                        break;
                    case PacketType.GetUsers:
                        {
                            var userList = (Dictionary<string, Tuple<string, string>>)packet.tag["userList"];
                            foreach(var user in userList)
                            {
                               var nodeResult = treeViewUsers.Nodes.OfType<TreeNode>().FirstOrDefault(node => node.Text.Equals(user.Value.Item2));
                               nodeResult.Nodes.Add(user.Key, user.Key, ClientSession.connectionStateHash[user.Value.Item1].Item1 + 1);
                            }
                            ClientSession.Connection.SendPacket(new Packet(PacketType.GetUserConversations));
                        }
                        break;
                    case PacketType.SetUserState:
                        {
                            TreeNode node = treeViewUsers.Nodes.Find(packet.tag["user"] as string, true)[0];
                            node.ImageIndex = ClientSession.connectionStateHash[packet.tag["state"] as string].Item1 + 1;
                        }
                        break;
                    case PacketType.SessionBegin:
                        {
                            Show();
                        }
                        break;
                }
            }
        }

        private void treeViewUsers_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            this.Header.Text = ClientSession.username + " - " + ClientSession.connectionStateHash[ClientSession.state].Item2;
            lbl_Usuario.Text = ClientSession.username;
            selectedStateItem = ((contextMenuStripEstado.Items[0] as ToolStripDropDownItem).DropDownItems[ClientSession.connectionStateHash[ClientSession.state].Item1] as ToolStripMenuItem);
            selectedStateItem.Checked = true;
            ClientSession.Connection.OnPacketReceivedFunc(OnPacket);
            ClientSession.Connection.SendPacket(new Packet(PacketType.GetUsers));
        }
        private void buttonNewGroupChat_Click(object sender, EventArgs e)
        {
            formCreateChat = new FormCreateChat();
            formCreateChat.ShowDialog();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
        
        private void conectadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetUserState(sender as ToolStripMenuItem, UserConnectionState.Available.ToString());
        }
        //No disponible
        private void ocupadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetUserState(sender as ToolStripMenuItem, UserConnectionState.NotAvailable.ToString());
        }
        //ocupado
        private void ocupadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetUserState(sender as ToolStripMenuItem, UserConnectionState.Busy.ToString());
        }

        private void desconectadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetUserState(sender as ToolStripMenuItem, UserConnectionState.Offline.ToString());
        }

        private void SetUserState(ToolStripMenuItem menuItem, string state)
        {
            if(!menuItem.Checked)
            {
                selectedStateItem.Checked = false;
                selectedStateItem = menuItem;
                selectedStateItem.Checked = true;
                Header.Text = ClientSession.username + " - " + ClientSession.connectionStateHash[ClientSession.state].Item2;
                Packet packet = new Packet(PacketType.SetUserState);
                packet.tag["user"] = ClientSession.username;
                packet.tag["state"] = state;
                ClientSession.Connection.SendPacket(packet);
            }
        }

        private void listViewConversacion_DoubleClick(object sender, EventArgs e)
        {
            (listViewConversacion.SelectedItems[0].Tag as formChat).Show();
        }
    }
    static class ClientSession
    {
        public delegate void ReceivePacketCallback(Packet packet);
        public static string username { get; set; }
        public static string state;
        public static Client Connection
        {
            get { return client; }
        }
        //connectionStateHash<key, tuple<imageIndex, nombre en español>>
        public static Dictionary<string, Tuple<int, string>> connectionStateHash = new Dictionary<string, Tuple<int,string>>();
        public static Dictionary<int, string> chats = new Dictionary<int, string>();
        public static Dictionary<string, UserConnectionState> userList = new Dictionary<string, UserConnectionState>();
        private static Client client = new Client();
    }
}
