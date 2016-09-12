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
        private string realText;
        private Point dragCursorPoint, dragFormPoint;
        public int chatID { get; set; }
        public List<string> users { get; set; }
        public ListViewItem listItem { get; set; }
        //http://csharpdemos.blogspot.mx/2012/10/how-to-insert-smiley-images-in.html
        private Dictionary<string, Bitmap> Emoticons;
        public formChat(int chatID, ListViewItem listItem)
        {
            this.chatID = chatID;
            this.listItem = listItem;
            Emoticons = new Dictionary<string, Bitmap>();
            Emoticons.Add(":)", ChatApp.Properties.Resources.happy);
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
        public void CheckEmoticons()
        {
            richTextBoxChat.ReadOnly = false;
            foreach (string emote in Emoticons.Keys)
            {
                while (richTextBoxChat.Text.Contains(emote))
                {
                    int index = richTextBoxChat.Text.IndexOf(emote);
                    richTextBoxChat.Select(index, emote.Length);
                    Clipboard.SetImage(Emoticons[emote]);
                    richTextBoxChat.Paste();
                }
            }
            richTextBoxChat.Select(richTextBoxChat.Text.Length, 0);
            richTextBoxChat.ScrollToCaret();
        }
        private void richTextBoxChat_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
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

        private void picBox_EmoteIcon_MouseEnter(object sender, EventArgs e) {
            picBox_EmoteIcon.BackgroundImage = ChatApp.Properties.Resources.emotIconHover;
        }

        private void picBox_EmoteIcon_MouseLeave(object sender, EventArgs e) {
            picBox_EmoteIcon.BackgroundImage = ChatApp.Properties.Resources.emotIcon;
        }

        private void picBox_Buzz_MouseEnter(object sender, EventArgs e) {
            picBox_Buzz.BackgroundImage = ChatApp.Properties.Resources.buzzIconHover;
        }

        private void picBox_Buzz_MouseLeave(object sender, EventArgs e) {
            picBox_Buzz.BackgroundImage = ChatApp.Properties.Resources.buzzIcon;
        }

        private void picBox_Buzz_MouseClick(object sender, MouseEventArgs e) {

        }

        private void picBox_Attach_MouseEnter(object sender, EventArgs e) {
            picBox_Attach.BackgroundImage = ChatApp.Properties.Resources.attachIconHover;
        }

        private void picBox_Attach_MouseLeave(object sender, EventArgs e) {
            picBox_Attach.BackgroundImage = ChatApp.Properties.Resources.attachIcon;
        }

        private void picBox_StartGame_MouseEnter(object sender, EventArgs e) {
            picBox_StartGame.BackgroundImage = ChatApp.Properties.Resources.gameIconHover;
        }

        private void picBox_StartGame_MouseLeave(object sender, EventArgs e) {
            picBox_StartGame.BackgroundImage = ChatApp.Properties.Resources.gameIcon;
        }

        private void TextBoxChat_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void formChat_MouseMove(object sender, MouseEventArgs e) {
            if (dragging) {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        //unused
        private void picBox_Attach_MouseClick(object sender, MouseEventArgs e) {

        }
        private void picBox_EmoteIcon_MouseClick(object sender, MouseEventArgs e) { }
        private void textBoxChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviar.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void picBox_StartGame_MouseClick(object sender, MouseEventArgs e) { }
    }
}
