using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyPOI;
using NAudio.Wave;
using mGame;
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
        private Thread gameThread;
        private POIGame game;
        private LevelStateOnline level;
        public class LevelStateOnline : LevelState
        {
            public LevelStateOnline(int player) : base(player) { }
            public override void PlayerInput(Direction direction)
            {
                UdpPacket udpPacket = new UdpPacket(UdpPacketType.PlayerInput);
                udpPacket.WriteData(BitConverter.GetBytes(ClientSession.GameSessionChatID));
                udpPacket.WriteData(BitConverter.GetBytes((int)direction));
                udpPacket.WriteData(BitConverter.GetBytes(ClientSession.IsPlayerOne ? 1 : 2));
                ClientSession.Connection.SendUdpPacket(udpPacket);
            }
            public void MovePlayer(Direction direction)
            {
                players[playerNumber == 1 ? 1 : 0].Move(direction);
            }
        }
        public class ConnectingState : GameState
        {
            private TextSprite text;
            private float elapse;
            public override void Init()
            {
                text = new TextSprite(Assets.retroFont, camera.Value.Center.ToVector2());
                text.text = "ESPERANDO A OTRO USUARIO...";
                text.origin = new Microsoft.Xna.Framework.Vector2(180, 10);
                AddGraphic(text);
                base.Init();
            }
            public override void Update()
            {
                elapse += (float)POIGame.DeltaTime * 0.7f;
                float effect = 1.0f + (float)Math.Sin(elapse) * 0.125f;
                text.scale = new Microsoft.Xna.Framework.Vector2(effect, effect);
                base.Update();
            }
        }
        public void Game()
        {
            //Packet packet = new Packet(PacketType.BeginGame);
            //ClientSession.Connection.SendPacket(packet);
            game = new POIGame(new ConnectingState());
            game.Run();
            ClientSession.GameIsRunning = false;
        }
        public FormHome()
        {
            InitializeComponent();
            listViewConversacion.Sorting = SortOrder.Ascending;
            listViewConversacion.ListViewItemSorter = new ListViewSorter(2);
            listViewPrivateMessages.Sorting = SortOrder.Ascending;
            listViewPrivateMessages.ListViewItemSorter = new ListViewSorter(1);
        }
        //Thread safe callbacks
        //http://stackoverflow.com/questions/10775367/cross-thread-operation-not-valid-control-textbox1-accessed-from-a-thread-othe
        internal void OnPacket(Packet packet)
        {
            if (this.InvokeRequired)
            {
                ClientSession.ReceivePacketCallback d = new ClientSession.ReceivePacketCallback(OnPacket);
                try
                {
                    this.Invoke(d, new object[] { packet });
                }
                catch
                {

                }
            }
            else
            {
                switch(packet.Type)
                {
                    case PacketType.GameFirstPlayer:
                        {
                            ClientSession.IsPlayerOne = true;
                            gameThread = new Thread(Game);
                            gameThread.Start();
                        }
                        break;
                    case PacketType.GameSecondPlayer:
                        {
                            if(!ClientSession.IsPlayerOne)
                            {
                                gameThread = new Thread(Game);
                                gameThread.Start();
                            }
                            else
                            {
                                level = new LevelStateOnline(1);
                                level.GenerateLevelData();
                                Packet packetSend = new Packet(PacketType.LevelData);
                                packetSend.tag["chatID"] = ClientSession.GameSessionChatID;
                                packetSend.tag["levelData"] = level.LevelData;
                                packetSend.tag["sender"] = ClientSession.username;
                                ClientSession.Connection.SendPacket(packetSend);
                                POIGame.SetState(level);
                            }
                        }
                        break;
                    case PacketType.LevelData:
                        {
                            level = new LevelStateOnline(2);
                            level.SetLevelData((UInt32[])packet.tag["levelData"]);
                            POIGame.SetState(level);
                        }
                        break;
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
                            chatsForms[chatID].Show();
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
                            privateChatForms[chatID].Show();
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
                            //remove self username
                            foreach (TreeNode carrer in treeViewUsers.Nodes)
                            {
                                carrer.Nodes.RemoveByKey(ClientSession.username);
                            }
                            treeViewUsers.ExpandAll();
                            Packet sendPacket = new Packet(PacketType.GetUserConversations);
                            sendPacket.tag["user"] = ClientSession.username;
                            ClientSession.Connection.SendPacket(sendPacket);
                        }
                        break;
                    case PacketType.SetUserState:
                        {
                            if(packet.tag["user"] as string != ClientSession.username)
                            {
                                TreeNode node = treeViewUsers.Nodes.Find(packet.tag["user"] as string, true)[0];
                                node.ImageIndex = ClientSession.connectionStateHash[packet.tag["state"] as string].Item1 + 1;
                            }
                        }
                        break;
                    case PacketType.SessionBegin:
                        {
                            Show();
                            Packet sendPacket = new Packet(PacketType.UdpLocalEndPoint);
                            sendPacket.tag["username"] = ClientSession.username;
                            sendPacket.tag["endPoint"] = ClientSession.Connection.UdpLocalEndPoint;
                            ClientSession.Connection.SendPacket(sendPacket);
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
                            if (packet.tag["sender"] as string != ClientSession.username
                                && privateChatForms.ContainsKey((int)packet.tag["chatID"]))
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
                    case PacketType.WebCamRequest:
                        {
                            if (packet.tag["sender"] as string != ClientSession.username)
                            {
                                DialogResult result = MessageBox.Show("¿Quieres comenzar una videollamada con ?" + packet.tag["sender"] as string, "Videollamada de " + packet.tag["sender"] as string, MessageBoxButtons.YesNo);
                                Packet sendPacket = new Packet(PacketType.WebCamResponse);
                                sendPacket.tag["chatID"] = packet.tag["chatID"];
                                sendPacket.tag["sender"] = ClientSession.username;
                                if(result == DialogResult.Yes)
                                {
                                    sendPacket.tag["response"] = true;
                                    int channels = (int)packet.tag["channels"];
                                    Speaker.Init(channels);
                                }
                                else
                                {
                                    sendPacket.tag["response"] = false;
                                }
                                ClientSession.Connection.SendPacket(sendPacket);
                            }
                        }
                        break;
                    case PacketType.WebCamResponse:
                        {
                            if (packet.tag["sender"] as string != ClientSession.username)
                            {
                                if((bool)packet.tag["response"])
                                    privateChatForms[(int)packet.tag["chatID"]].StartWebcam();
                            }
                        }
                        break;
                }
            }
        }

        internal void OnUdpPacket(UdpPacket packet)
        {
            if (this.InvokeRequired)
            {
                ClientSession.ReceiveUdpPacketCallback d = new ClientSession.ReceiveUdpPacketCallback(OnUdpPacket);
                try
                {
                    this.Invoke(d, new object[] { packet });
                }
                catch
                {

                }
            }
            else
            {
                switch(packet.PacketType)
                {
                    case UdpPacketType.AudioStream:
                        {
                            int chatID = packet.ReadInt(0);
                            int audioStreamLenght = packet.ReadInt(4);
                            byte[] audioStream = packet.ReadData(audioStreamLenght, 8);
                            //int stringLenght = UdpPacket.ReadInt(bytes, 8 + audioStreamLenght);
                            //string username = Encoding.Unicode.GetString(packet.ReadData(stringLenght, 12 + audioStreamLenght));
                            Speaker.PlayBuffer(audioStream);
                        }
                        break;
                    case UdpPacketType.PlayerInput:
                        {
                            int chatID = packet.ReadInt(0);
                            Direction direction = (Direction)packet.ReadInt(4);
                            //int player = packet.ReadInt(8);
                            level.MovePlayer(direction);
                        }
                        break;
                }
            }
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

        private void picBox_CloseIcon_MouseClick(object sender, MouseEventArgs e) {
            if (ClientSession.HasCamera) {
                Camera.Release();
                Microphone.Dispose();
            }
            Speaker.Dispose();
            this.Close();
        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            this.Header.Text = ClientSession.username + " - " + ClientSession.connectionStateHash[ClientSession.state].Item2;
            lbl_Jugador.Text = ClientSession.username;
            selectedStateItem = ((contextMenuStripEstado.Items[0] as ToolStripDropDownItem).DropDownItems[ClientSession.connectionStateHash[ClientSession.state].Item1] as ToolStripMenuItem);
            selectedStateItem.Checked = true;
            ClientSession.Connection.OnPacketReceivedFunc(OnPacket);
            ClientSession.Connection.SetOnUdpPacketReceived(OnUdpPacket);
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
            //gameThread = new Thread(Game);
            //gameThread.Start();
            //using (var game = new POIGame()) {
            //    game.Run();
            //}
            //Speaker.Init();
        }

        private void buttonNewGroupChat_Click(object sender, EventArgs e)
        {
            formCreateChat = new FormCreateChat();
            formCreateChat.ShowDialog();
        }
        //disponible
        private void conectadoToolStripMenuItem_Click(object sender, EventArgs e){SetUserState(sender as ToolStripMenuItem, UserConnectionState.Available.ToString());}
        //no disponible
        private void ocupadoToolStripMenuItem_Click(object sender, EventArgs e){SetUserState(sender as ToolStripMenuItem, UserConnectionState.NotAvailable.ToString());}
        //ocupado
        private void ocupadoToolStripMenuItem1_Click(object sender, EventArgs e){SetUserState(sender as ToolStripMenuItem, UserConnectionState.Busy.ToString());}
        //desconectado
        private void desconectadoToolStripMenuItem_Click(object sender, EventArgs e){SetUserState(sender as ToolStripMenuItem, UserConnectionState.Offline.ToString());}

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

        private void button1_Click(object sender, EventArgs e)
        {
            FormPrivateMessage form = new FormPrivateMessage();
            form.ShowDialog();
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
        private void listViewConversacion_Click(object sender, EventArgs e) { (listViewConversacion.SelectedItems[0].Tag as formChat).Show(); }
        private void listViewPrivateMessages_Click(object sender, EventArgs e) { (listViewPrivateMessages.SelectedItems[0].Tag as FormPrivateChat).Show(); }
        private void FormHome_MouseUp(object sender, MouseEventArgs e) { dragging = false; }
        private void picBox_CloseIcon_MouseEnter(object sender, EventArgs e) { picBox_CloseIcon.BackColor = Color.Brown; }
        private void picBox_CloseIcon_MouseLeave(object sender, EventArgs e) { picBox_CloseIcon.BackColor = Color.White; }
        //unused
        //private void listViewConversacion_SelectedIndexChanged(object sender, EventArgs e) { }
        private void toolTip1_Popup(object sender, PopupEventArgs e) { }
        private void treeViewUsers_AfterSelect(object sender, TreeViewEventArgs e) { }
    }
    static class ClientSession
    {
        public delegate void ReceivePacketCallback(Packet packet);
        public delegate void ReceiveUdpPacketCallback(UdpPacket packet);
        public static string username { get; set; }
        public static string state;
        public const int textMessagesVisibleText = 40;
        public static bool HasCamera;
        public static bool GameIsRunning;
        public static bool IsPlayerOne;
        public static int GameSessionChatID = -1;
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
