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

        public formChat()
        {
            InitializeComponent();
        }

        private void buttonEnviar_Click(object sender, EventArgs e)
        {
            TextMessage message = new TextMessage();
            message.destination = "otroUsuario";
            message.sender = ClientSession.username;
            message.message = textBoxChat.Text;
            ClientSession.Connection.SendPacket(new Packet(message));

            textBoxChat.Text = "";
        }

        private void formChat_Load(object sender, EventArgs e)
        {
            ClientSession.Connection.OnPacketReceivedFunc(ReceivePacket);
            textBoxChat.Focus();
        }
        //Thread safe callbacks
        //http://stackoverflow.com/questions/10775367/cross-thread-operation-not-valid-control-textbox1-accessed-from-a-thread-othe
        //private delegate void ReceivePacketCallback(Packet packet);
        private void ReceivePacket(Packet packet)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (packet.Content.Type == PacketType.TextMessage)
            {
                TextMessage message = packet.Content as TextMessage;
                if (this.richTextBoxChat.InvokeRequired)
                {
                    ClientSession.ReceivePacketCallback d = new ClientSession.ReceivePacketCallback(ReceivePacket);
                    this.Invoke(d, new object[] { packet });
                }
                else
                {
                    richTextBoxChat.Text += "\n" + message.sender + ": " + message.message;
                }
            }
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
            this.Close();
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

        private void formChat_MouseMove(object sender, MouseEventArgs e) {
            if (dragging) {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
    }
}
