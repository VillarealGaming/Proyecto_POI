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
            listViewConversacion.Sorting = SortOrder.Ascending;
            listViewConversacion.ListViewItemSorter = new ListViewSorter(2);
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
                            Dictionary<int, List<Tuple<string, string>>> text = packet.tag["messages"] as Dictionary<int, List<Tuple<string, string>>>;
                            Dictionary<int, List<string>> users = packet.tag["users"] as Dictionary<int, List<string>>;
                            Dictionary<int, DateTime> lastMessageDate = packet.tag["lastDate"] as Dictionary<int, DateTime>;
                            //Conversations
                            foreach (var c in ClientSession.chats)
                            {
                                var item = listViewConversacion.Items.Add(new ListViewItem(new string[] { c.Value, "" , "" }));
                                chatsForms.Add(c.Key, new formChat(c.Key, item));
                                chatsForms[c.Key].Header.Text = c.Value;
                                item.Tag = chatsForms[c.Key];
                                //Messages
                                foreach (var message in text[c.Key])
                                {
                                    var chatRichTextBox = chatsForms[c.Key].richTextBoxChat;
                                    chatRichTextBox.SelectionFont = new Font(chatRichTextBox.Font, FontStyle.Bold);
                                    chatRichTextBox.AppendText(message.Item2 + ": ");
                                    chatRichTextBox.SelectionFont = new Font(chatRichTextBox.Font, FontStyle.Regular);
                                    chatRichTextBox.AppendText(message.Item1 + "\n");
                                }
                                //Last message
                                if (text[c.Key].Count > 0)
                                {
                                    chatsForms[c.Key].listItem.SubItems[1].Text = text[c.Key].Last().Item2 + ": " + text[c.Key].Last().Item1;
                                    chatsForms[c.Key].listItem.SubItems[2].Tag = lastMessageDate[c.Key];
                                    chatsForms[c.Key].listItem.SubItems[2].Text = lastMessageDate[c.Key].ToString();
                                }
                                //sort by date
                                listViewConversacion.Sort();
                                //Users
                                foreach (var user in users[c.Key]){ chatsForms[c.Key].listViewUsers.Items.Add(user); }
                                //Set converstaion emoticons
                                chatsForms[c.Key].CheckEmoticons();
                            }
                            Packet sendPacket = new Packet(PacketType.SetUserState);
                            sendPacket.tag["user"] = ClientSession.username;
                            sendPacket.tag["state"] = ClientSession.state;
                            ClientSession.Connection.SendPacket(sendPacket);
                        }
                        break;
                    case PacketType.TextMessage:
                        {
                            int chatID = (int)packet.tag["chatID"];
                            DateTime date = (DateTime)packet.tag["date"];
                            var chatRichTextBox = chatsForms[chatID].richTextBoxChat;
                            chatRichTextBox.SelectionFont = new Font(chatRichTextBox.Font, FontStyle.Bold);
                            chatRichTextBox.AppendText(packet.tag["sender"] + ": ");
                            chatRichTextBox.SelectionFont = new Font(chatRichTextBox.Font, FontStyle.Regular);
                            chatRichTextBox.AppendText(packet.tag["text"] + "\n");
                            chatsForms[chatID].listItem.SubItems[1].Text = packet.tag["sender"] + ": " + packet.tag["text"];
                            chatsForms[chatID].CheckEmoticons();
                            //sort by date
                            chatsForms[chatID].listItem.SubItems[2].Tag = date;
                            chatsForms[chatID].listItem.SubItems[2].Text = date.ToString();
                            listViewConversacion.Sort();
                        }
                        break;
                    case PacketType.GetUsers:
                        {
                            var userList = (Dictionary<string, Tuple<string, string>>)packet.tag["userList"];
                            foreach(var user in userList)
                            {
                                //tree nodes
                                var nodeResult = treeViewUsers.Nodes.OfType<TreeNode>().FirstOrDefault(node => node.Text.Equals(user.Value.Item2));
                                TreeNode insertedNode = nodeResult.Nodes.Add(user.Key, user.Key, ClientSession.connectionStateHash[user.Value.Item1].Item1 + 1);
                                //font
                                insertedNode.NodeFont = new Font(treeViewUsers.Font,FontStyle.Regular);
                                insertedNode.ForeColor = Color.LightGray;
                                //userList
                                ClientSession.userList.Add(user.Key);
                            }
                            treeViewUsers.ExpandAll();
                            Packet sendPacket = new Packet(PacketType.GetUserConversations);
                            sendPacket.tag["user"] = ClientSession.username;
                            ClientSession.Connection.SendPacket(sendPacket);
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
                    case PacketType.Fail:
                        {
                            MessageBox.Show(packet.tag["message"] as string, packet.tag["case"] as string, MessageBoxButtons.OK);
                            formCreateChat.Close();
                        }
                        break;
                    case PacketType.Buzz:
                        {
                            if(packet.tag["sender"] as string != ClientSession.username)
                            {
                                chatsForms[(int)packet.tag["chatID"]].Buzz();
                            }
                            else
                            {
                                Packet packetSend = new Packet(PacketType.TextMessage);
                                packetSend.tag["sender"] = packet.tag["sender"];
                                packetSend.tag["text"] = ".-*Zumbido*-.";
                                packetSend.tag["chatID"] = packet.tag["chatID"];
                                packetSend.tag["date"] = DateTime.Now;
                                ClientSession.Connection.SendPacket(packetSend);
                            }
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
            selectedStateItem = ((contextMenuStripEstado.Items[0] as ToolStripDropDownItem).DropDownItems[ClientSession.connectionStateHash[ClientSession.state].Item1] as ToolStripMenuItem);
            selectedStateItem.Checked = true;
            ClientSession.Connection.OnPacketReceivedFunc(OnPacket);
            ClientSession.Connection.SendPacket(new Packet(PacketType.GetUsers));
            //Emoticons
            formChat.Emoticons = new Dictionary<Bitmap, string[]>();
            formChat.Emoticons.Add(ChatApp.Properties.Resources.angry, new string[] { " >:(", ":angry:" });
            formChat.Emoticons.Add(ChatApp.Properties.Resources.cool, new string[] { " 8)", ":cool:" });
            formChat.Emoticons.Add(ChatApp.Properties.Resources.devil, new string[] { " >:)", ":devil:" });
            formChat.Emoticons.Add(ChatApp.Properties.Resources.dumb, new string[] { " :P", ":dumb:" });
            formChat.Emoticons.Add(ChatApp.Properties.Resources.happy, new string[] { " :)", ":happy:" });
            formChat.Emoticons.Add(ChatApp.Properties.Resources.meh, new string[] { " :/", ":meh:" });
            formChat.Emoticons.Add(ChatApp.Properties.Resources.naughty, new string[] { " ^O^", ":naughty:" });
            formChat.Emoticons.Add(ChatApp.Properties.Resources.sad, new string[] { " :(", ":sad:" });
            formChat.Emoticons.Add(ChatApp.Properties.Resources.serious, new string[] { " :I", " .-.", " ._.", ":serious:" });
            formChat.Emoticons.Add(ChatApp.Properties.Resources.smile, new string[] { " :D", ":smile:" });
            formChat.Emoticons.Add(ChatApp.Properties.Resources.surprise, new string[] { " :O", " :o", " :0", ":surprise:" });
            formChat.Emoticons.Add(ChatApp.Properties.Resources.weird, new string[] { " :$", " .~.", ":weird:" });
            formChat.Emoticons.Add(ChatApp.Properties.Resources.wink, new string[] { " ;)", ":wink:" });
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
                ClientSession.state = state;
                Header.Text = ClientSession.username + " - " + ClientSession.connectionStateHash[ClientSession.state].Item2;
                Packet packet = new Packet(PacketType.SetUserState);
                packet.tag["user"] = ClientSession.username;
                packet.tag["state"] = state;
                ClientSession.Connection.SendPacket(packet);
            }
        }

        private void listViewConversacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listViewConversacion_DoubleClick(object sender, EventArgs e)
        {
            (listViewConversacion.SelectedItems[0].Tag as formChat).Show();
        }
        //sorting listview columns
        //http://stackoverflow.com/questions/1214289/how-do-i-sort-integers-in-a-listview
        private class ListViewSorter : System.Collections.IComparer
        {
            private int _colIndex;
            public ListViewSorter(int colIndex) { _colIndex = colIndex; }
            public int Compare(object x, object y)
            {
                var item1 = (ListViewItem)x;
                var item2 = (ListViewItem)y;
                if (item2.SubItems[_colIndex].Tag == null)
                    return 0;
                if (item1.SubItems[_colIndex].Tag == null)
                    return 1;
                if (item1.SubItems[_colIndex].Tag is DateTime)
                {
                    DateTime date1 = (DateTime)item1.SubItems[_colIndex].Tag;
                    DateTime date2 = (DateTime)item2.SubItems[_colIndex].Tag;
                    return date2.CompareTo(date1);
                }
                else
                {
                    return 0;
                }
            }
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
        private static List<Packet> packetQueue = new List<Packet>();
        //connectionStateHash<key, tuple<imageIndex, nombre en español>>
        public static Dictionary<string, Tuple<int, string>> connectionStateHash = new Dictionary<string, Tuple<int,string>>();
        public static Dictionary<int, string> chats = new Dictionary<int, string>();
        //public static Dictionary<string, UserConnectionState> userList = new Dictionary<string, UserConnectionState>();
        public static List<string> userList = new List<string>();
        private static Client client = new Client();
    }
}
