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
        private FormCreateChat formCreateChat;
        private Dictionary<int, formChat> chatsForms = new Dictionary<int, formChat>();

        public FormHome()
        {
            InitializeComponent();
        }
        //Thread safe callbacks
        //http://stackoverflow.com/questions/10775367/cross-thread-operation-not-valid-control-textbox1-accessed-from-a-thread-othe
        private void OnPacket(Packet packet)
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
                            chatsForms[chatID].listItem.SubItems[1].Text = messages.Last().Item2 + ": " + messages.Last().Item1;
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
                }
            }
        }

        private void treeViewUsers_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void FormHome_Load(object sender, EventArgs e)
        {
            ClientSession.Connection.OnPacketReceivedFunc(OnPacket);
            ClientSession.Connection.SendPacket(new Packet(PacketType.GetUserConversations));
        }

        private void buttonNewGroupChat_Click(object sender, EventArgs e)
        {
            formCreateChat = new FormCreateChat();
            formCreateChat.ShowDialog();
        }

        private void listViewConversacion_SelectedIndexChanged(object sender, EventArgs e)
        {

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
        public static Client Connection
        {
            get { return client; }
        }
        public static Dictionary<int, string> chats = new Dictionary<int, string>();
        public static List<string> userList = new List<string>();
        private static Client client = new Client();
    }
}
