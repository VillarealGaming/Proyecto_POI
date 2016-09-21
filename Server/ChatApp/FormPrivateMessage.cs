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
    public partial class FormPrivateMessage : Form
    {
        private AutoCompleteStringCollection autoCompleteUsers;
        public FormPrivateMessage()
        {
            InitializeComponent();
            autoCompleteUsers = new AutoCompleteStringCollection();
            autoCompleteUsers.AddRange(ClientSession.userList.ToArray());
            autoCompleteUsers.Remove(ClientSession.username);
            textBoxUser.AutoCompleteCustomSource = autoCompleteUsers;
            textBoxUser.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxUser.AutoCompleteSource = AutoCompleteSource.CustomSource;
            ValidateChat();
        }

        private void ValidateChat()
        {
            buttonCreateChat.Enabled = !string.IsNullOrWhiteSpace(textBoxChat.Text);
        }

        private void picBox_CloseIcon_MouseClick(object sender, MouseEventArgs e){ this.Close(); }

        private void buttonCreateChat_Click(object sender, EventArgs e)
        {
            //Check if private chat exists.
            var list = new List<string>();
            list.Add(ClientSession.username);
            list.Add(textBoxUser.Text);
            //http://stackoverflow.com/questions/1520642/does-net-have-a-way-to-check-if-list-a-contains-all-items-in-list-b
            var queryResult = from privateChat in ClientSession.privateChats
                              where !privateChat.Value.Except(list).Any()
                              select privateChat;
            Packet packet;
            if (queryResult.Count() == 1)
            {
                //Send message
                packet = new Packet(PacketType.PrivateTextMessage);
                packet.tag["chatID"] = queryResult.First().Key;
                //packet.tag["users"] = list;
                packet.tag["sender"] = ClientSession.username;
                packet.tag["text"] = textBoxChat.Text;
                packet.tag["date"] = DateTime.Now;
                packet.tag["encriptado"] = checkBoxEncrypt.Checked;
                ClientSession.Connection.SendPacket(packet);
            }
            else
            {
                //Create private chat
                packet = new Packet(PacketType.CreatePrivateConversation);
                packet.tag["users"] = list;
                packet.tag["sender"] = ClientSession.username;
                packet.tag["text"] = textBoxChat.Text;
                packet.tag["date"] = DateTime.Now;
                packet.tag["encriptado"] = checkBoxEncrypt.Checked;
                ClientSession.Connection.SendPacket(packet);
            }
            this.Close();
        }

        private void textBoxUser_TextChanged(object sender, EventArgs e)
        {
            buttonCreateChat.Enabled = ClientSession.userList.Contains(textBoxUser.Text);
            ValidateChat();
        }

        private void textBoxChat_TextChanged(object sender, EventArgs e)
        {
            ValidateChat();
            int i = 0;
        }
    }
}
