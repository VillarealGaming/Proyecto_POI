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
    public partial class formChat : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;
        public int chatID { get; set; }
        public ListViewItem listItem { get; set; }
        public formChat(int chatID, ListViewItem listItem)
        {
            Packet packet = new Packet(PacketType.GetChatConversation);
            packet.tag["chatID"] = chatID;
            this.chatID = chatID;
            this.listItem = listItem;
            ClientSession.Connection.SendPacket(packet);
            InitializeComponent();
        }

        private void buttonEnviar_Click(object sender, EventArgs e)
        {
            Packet packet = new Packet(PacketType.TextMessage);
            packet.tag["destination"] = "otroUsuario";
            packet.tag["sender"] = ClientSession.username;
            packet.tag["text"] = textBoxChat.Text;
            packet.tag["chatID"] = chatID;
            packet.tag["date"] = DateTime.Now;
            ClientSession.Connection.SendPacket(packet);

            textBoxChat.Text = "";
        }

        private void formChat_Load(object sender, EventArgs e)
        {
            textBoxChat.Focus();
        }

        private void richTextBoxChat_TextChanged(object sender, EventArgs e)
        {
            richTextBoxChat.SelectionStart = richTextBoxChat.Text.Length;
            richTextBoxChat.ScrollToCaret();
        }

        private void formChat_MouseDown(object sender, MouseEventArgs e) {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void formChat_MouseUp(object sender, MouseEventArgs e) {
            dragging = false;
        }

        private void picBox_CloseIcon_MouseEnter(object sender, EventArgs e) {
            picBox_CloseIcon.BackColor = Color.Brown;
        }

        private void picBox_CloseIcon_MouseLeave(object sender, EventArgs e) {
            picBox_CloseIcon.BackColor = Color.White;
        }

        private void picBox_CloseIcon_MouseClick(object sender, MouseEventArgs e) {
            this.Hide();
        }

        private void textBoxChat_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                buttonEnviar.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
       
        private void picBox_EmoteIcon_MouseEnter(object sender, EventArgs e) {
            picBox_EmoteIcon.BackgroundImage = ChatApp.Properties.Resources.emotIconHover;
        }

        private void picBox_EmoteIcon_MouseLeave(object sender, EventArgs e) {
            picBox_EmoteIcon.BackgroundImage = ChatApp.Properties.Resources.emotIcon;
        }

        private void textBoxChat_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void formChat_MouseMove(object sender, MouseEventArgs e) {
            if (dragging) {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
    }
}
