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
        private Dictionary<int, FormPrivateChat> privateChatForms = new Dictionary<int, FormPrivateChat>();
        private ToolStripMenuItem selectedStateItem;
        public FormHome()
        {
            InitializeComponent();
            listViewConversacion.Sorting = SortOrder.Ascending;
            listViewConversacion.ListViewItemSorter = new ListViewSorter(2);
            listViewPrivateMessages.Sorting = SortOrder.Ascending;
            listViewPrivateMessages.ListViewItemSorter = new ListViewSorter(1);
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
            if(ClientSession.HasCamera)
            {
                Camera.Release();
            }
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
                    case PacketType.CreatePrivateConversation:
                        {
                            List<string> users = (List<string>)packet.tag["users"];
                            string user = (from u in users where u != ClientSession.username select u).ToList().Last();
                            int chatID = (int)packet.tag["id"];
                            var item = listViewPrivateMessages.Items.Add(new ListViewItem(new string[] { user, " ", " " }));
                            privateChatForms.Add(chatID, new FormPrivateChat(chatID, item));
                            ClientSession.privateChats.Add(chatID, users);
                            item.Tag = privateChatForms[chatID];
                            //Enviar mensaje despues de crear la conversación
                            if(ClientSession.username == packet.tag["sender"] as string)
                            {
                                Packet packetSend = new Packet(PacketType.PrivateTextMessage);
                                packetSend.tag["chatID"] = chatID;
                                packetSend.tag["sender"] = ClientSession.username;
                                //packetSend.tag["users"] = users;
                                packetSend.tag["text"] = packet.tag["text"];
                                packetSend.tag["date"] = packet.tag["date"];
                                packetSend.tag["encriptado"] = packet.tag["encriptado"];
                                ClientSession.Connection.SendPacket(packetSend);
                            }
                        }
                        break;
                    case PacketType.CreatePublicConversation:
                        {
                            var item = listViewConversacion.Items.Add(new ListViewItem(new string[] {packet.tag["nombre"] as string, " " }));
                            int chatID = (int)packet.tag["id"];
                            chatsForms.Add(chatID, new formChat(chatID, item));
                            ClientSession.chats.Add(chatID, packet.tag["nombre"] as string);
                            item.Tag = chatsForms[chatID];
                            formCreateChat.Close();
                        }
                        break;
                    case PacketType.GetPrivateConversations:
                        {
                            ClientSession.privateChats = packet.tag["conversations"] as Dictionary<int, List<string>>;
                            Dictionary<int, List<Tuple<string, string>>> text = packet.tag["messages"] as Dictionary<int, List<Tuple<string, string>>>;
                            Dictionary<int, DateTime> lastMessageDate = packet.tag["lastDate"] as Dictionary<int, DateTime>;
                            //Conversations
                            foreach (var c in ClientSession.privateChats)
                            { 
                                string user = (from u in c.Value where u != ClientSession.username select u).ToList().Last();
                                var item = listViewPrivateMessages.Items.Add(new ListViewItem(new string[] { user, "", "" }));
                                privateChatForms.Add(c.Key, new FormPrivateChat(c.Key, item));
                                privateChatForms[c.Key].Header.Text = user;
                                item.Tag = privateChatForms[c.Key];
                                //Messages
                                foreach (var message in text[c.Key]) { privateChatForms[c.Key].AddMessageToChat(message.Item2, message.Item1); }
                                //Set converstaion emoticons
                                privateChatForms[c.Key].CheckEmoticons();
                                //Last message
                                if (text[c.Key].Count > 0)
                                    privateChatForms[c.Key].SetLastMessage(text[c.Key].Last().Item2, text[c.Key].Last().Item1, lastMessageDate[c.Key]);
                                //sort by date
                                listViewPrivateMessages.Sort();
                            }
                            Packet sendPacket = new Packet(PacketType.SetUserState);
                            sendPacket.tag["user"] = ClientSession.username;
                            sendPacket.tag["state"] = ClientSession.state;
                            ClientSession.Connection.SendPacket(sendPacket);
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
                                foreach (var message in text[c.Key]){ chatsForms[c.Key].AddMessageToChat(message.Item2, message.Item1); }
                                //Set converstaion emoticons
                                chatsForms[c.Key].CheckEmoticons();
                                //Last message
                                if (text[c.Key].Count > 0)
                                    chatsForms[c.Key].SetLastMessage(text[c.Key].Last().Item2, text[c.Key].Last().Item1, lastMessageDate[c.Key]);
                                //sort by date
                                listViewConversacion.Sort();
                                //Users
                                foreach (var user in users[c.Key]){ chatsForms[c.Key].listViewUsers.Items.Add(user); }
                            }
                            Packet sendPacket = new Packet(PacketType.GetPrivateConversations);
                            sendPacket.tag["user"] = ClientSession.username;
                            ClientSession.Connection.SendPacket(sendPacket);
                        }
                        break;
                    case PacketType.TextMessage:
                        {
                            int chatID = (int)packet.tag["chatID"];
                            DateTime date = (DateTime)packet.tag["date"];
                            chatsForms[chatID].AddMessageToChat(packet.tag["sender"] as string, packet.tag["text"] as string);
                            chatsForms[chatID].SetLastMessage(packet.tag["sender"] as string, packet.tag["text"] as string, date);
                            chatsForms[chatID].CheckEmoticons();
                            listViewConversacion.Sort();
                        }
                        break;
                    case PacketType.PrivateTextMessage:
                        {
                            int chatID = (int)packet.tag["chatID"];
                            DateTime date = (DateTime)packet.tag["date"];
                            privateChatForms[chatID].AddMessageToChat(packet.tag["sender"] as string, packet.tag["text"] as string);
                            privateChatForms[chatID].SetLastMessage(packet.tag["sender"] as string, packet.tag["text"] as string, date);
                            privateChatForms[chatID].CheckEmoticons();
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
                    case PacketType.PrivateBuzz:
                        {
                            if (packet.tag["sender"] as string != ClientSession.username)
                            {
                                privateChatForms[(int)packet.tag["chatID"]].Buzz();
                            }
                            else
                            {
                                Packet packetSend = new Packet(PacketType.PrivateTextMessage);
                                packetSend.tag["sender"] = packet.tag["sender"];
                                packetSend.tag["text"] = ".-*Zumbido*-.";
                                packetSend.tag["chatID"] = packet.tag["chatID"];
                                packetSend.tag["date"] = DateTime.Now;
                                packetSend.tag["encriptado"] = false;
                                ClientSession.Connection.SendPacket(packetSend);
                            }
                        }
                        break;
                    case PacketType.FileSendPrivate:
                        if (packet.tag["sender"] as string != ClientSession.username)
                        {
                            //save file
                            privateChatForms[(int)packet.tag["chatID"]].ReceiveFile(packet);
                        }
                        else
                        {
                            Packet packetSend = new Packet(PacketType.PrivateTextMessage);
                            packetSend.tag["sender"] = packet.tag["sender"];
                            packetSend.tag["text"] = "--Archivo adjunto enviado--";
                            packetSend.tag["chatID"] = packet.tag["chatID"];
                            packetSend.tag["date"] = DateTime.Now;
                            packetSend.tag["encriptado"] = false;
                            ClientSession.Connection.SendPacket(packetSend);
                        }
                        break;
                    case PacketType.WebCamFrame:
                        {
                            if (packet.tag["sender"] as string != ClientSession.username)
                                privateChatForms[(int)packet.tag["chatID"]].ReceiveCameraPacket(packet);
                            else
                                Camera.CanSend = true;
                        }
                        break;
                    case PacketType.FileSendChat:
                        {
                            if (packet.tag["sender"] as string != ClientSession.username)
                            {
                                //save file
                                chatsForms[(int)packet.tag["chatID"]].ReceiveFile(packet);
                            }
                            else
                            {
                                Packet packetSend = new Packet(PacketType.TextMessage);
                                packetSend.tag["sender"] = packet.tag["sender"];
                                packetSend.tag["text"] = "--Archivo adjunto enviado--";
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
            lbl_Jugador.Text = ClientSession.username;
            selectedStateItem = ((contextMenuStripEstado.Items[0] as ToolStripDropDownItem).DropDownItems[ClientSession.connectionStateHash[ClientSession.state].Item1] as ToolStripMenuItem);
            selectedStateItem.Checked = true;
            ClientSession.Connection.OnPacketReceivedFunc(OnPacket);
            ClientSession.Connection.SendPacket(new Packet(PacketType.GetUsers));
            //Emoticons
            ClientSession.Emoticons = new Dictionary<Bitmap, string[]>();
            ClientSession.Emoticons.Add(ChatApp.Properties.Resources.angry, new string[] { " >:(", ":angry:" });
            ClientSession.Emoticons.Add(ChatApp.Properties.Resources.cool, new string[] { " 8)", ":cool:" });
            ClientSession.Emoticons.Add(ChatApp.Properties.Resources.devil, new string[] { " >:)", ":devil:" });
            ClientSession.Emoticons.Add(ChatApp.Properties.Resources.dumb, new string[] { " :P", ":dumb:" });
            ClientSession.Emoticons.Add(ChatApp.Properties.Resources.happy, new string[] { " :)", ":happy:" });
            ClientSession.Emoticons.Add(ChatApp.Properties.Resources.meh, new string[] { " :/", ":meh:" });
            ClientSession.Emoticons.Add(ChatApp.Properties.Resources.naughty, new string[] { " ^O^", ":naughty:" });
            ClientSession.Emoticons.Add(ChatApp.Properties.Resources.sad, new string[] { " :(", ":sad:" });
            ClientSession.Emoticons.Add(ChatApp.Properties.Resources.serious, new string[] { " :I", " .-.", " ._.", ":serious:" });
            ClientSession.Emoticons.Add(ChatApp.Properties.Resources.smile, new string[] { " :D", ":smile:" });
            ClientSession.Emoticons.Add(ChatApp.Properties.Resources.surprise, new string[] { " :O", " :o", " :0", ":surprise:" });
            ClientSession.Emoticons.Add(ChatApp.Properties.Resources.weird, new string[] { " :$", " .~.", ":weird:" });
            ClientSession.Emoticons.Add(ChatApp.Properties.Resources.wink, new string[] { " ;)", ":wink:" });
            ClientSession.HasCamera = Camera.Detect();
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

        private void button1_Click(object sender, EventArgs e)
        {
            FormPrivateMessage form = new FormPrivateMessage();
            form.ShowDialog();
        }

        private void listViewConversacion_Click(object sender, EventArgs e)
        {
            (listViewConversacion.SelectedItems[0].Tag as formChat).Show();
        }

        private void listViewPrivateMessages_Click(object sender, EventArgs e)
        {
            (listViewPrivateMessages.SelectedItems[0].Tag as FormPrivateChat).Show();
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
                    return 1;
                if (item1.SubItems[_colIndex].Tag == null)
                    return 0;
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
        public const int textMessagesVisibleText = 40;
        public static bool HasCamera;
        public static Client Connection
        {
            get { return client; }
        }
        //connectionStateHash<key, tuple<imageIndex, nombre en español>>
        public static Dictionary<string, Tuple<int, string>> connectionStateHash = new Dictionary<string, Tuple<int,string>>();
        public static Dictionary<int, string> chats = new Dictionary<int, string>();
        public static Dictionary<int, List<string>> privateChats = new Dictionary<int, List<string>>();
        public static List<string> userList = new List<string>();
        private static Client client = new Client();
        //http://csharpdemos.blogspot.mx/2012/10/how-to-insert-smiley-images-in.html
        public static Dictionary<Bitmap, string[]> Emoticons;
    }
}
