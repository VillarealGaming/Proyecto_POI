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
    public partial class FormCreateChat : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;
        public FormCreateChat()
        {
            InitializeComponent();
        }

        private void buttonCreateChat_Click(object sender, EventArgs e)
        {
            if(textBoxChatName.Text.Length != 0)
            {
                Packet packet = new Packet(PacketType.CreatePublicConversation);
                packet.tag["nombre"] = textBoxChatName.Text;
                packet.tag["encriptado"] = checkBoxEncrypt.Checked;
                ClientSession.Connection.SendPacket(packet);
            }
        }

        private void FormCreateChat_Load(object sender, EventArgs e)
        {
            //ClientSession.Connection.OnPacketReceivedFunc(OnPacket);//
        }

        private void FormCreateChat_MouseDown(object sender, MouseEventArgs e) {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void FormCreateChat_MouseMove(object sender, MouseEventArgs e) {
            if (dragging) {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void FormCreateChat_MouseUp(object sender, MouseEventArgs e) {
            dragging = false;
        }

        private void picBox_CloseIcon_MouseEnter(object sender, EventArgs e) {
            picBox_CloseIcon.BackColor = Color.Brown;
        }

        private void picBox_CloseIcon_MouseLeave(object sender, EventArgs e) {
            picBox_CloseIcon.BackColor = Color.White;
        }

        private void textBoxChatName_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                buttonCreateChat.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void picBox_CloseIcon_MouseClick(object sender, MouseEventArgs e) {
            this.Close();
        }
    }
}
