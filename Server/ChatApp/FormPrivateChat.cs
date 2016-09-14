using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class FormPrivateChat : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;

        public FormPrivateChat()
        {
            InitializeComponent();
        }

        private void FormPrivateChat_MouseDown(object sender, MouseEventArgs e) {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void FormPrivateChat_MouseMove(object sender, MouseEventArgs e) {
            if (dragging) {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void FormPrivateChat_MouseUp(object sender, MouseEventArgs e) {
            dragging = false;
        }

        private void picBox_CloseIcon_MouseClick(object sender, MouseEventArgs e) {
            this.Hide();
        }

        private void picBox_CloseIcon_MouseEnter(object sender, EventArgs e) {
            picBox_CloseIcon.BackColor = Color.Brown;
        }

        private void textBoxChat_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                buttonEnviar.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void picBox_Options_Click(object sender, EventArgs e) {
            list_Options.Visible = !list_Options.Visible;
            if (list_Options.Visible) {
                list_Options.Focus();
            }
        }

        private void list_Options_Leave(object sender, EventArgs e) {
            list_Options.Visible = false;
        }

        private void list_Options_SelectedIndexChanged(object sender, EventArgs e) {
            list_Options.Visible = false;
            textBoxChat.Focus();
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

        private void buttonEnviar_Click(object sender, EventArgs e) {

        }

        private void picBox_Attach_MouseEnter(object sender, EventArgs e) {
            picBox_Attach.BackgroundImage = ChatApp.Properties.Resources.attachIconHover;
        }

        private void picBox_Attach_MouseLeave(object sender, EventArgs e) {
            picBox_Attach.BackgroundImage = ChatApp.Properties.Resources.attachIcon;
        }

        private void picBox_Attach_MouseClick(object sender, MouseEventArgs e) {

        }

        private void picBox_StartGame_MouseEnter(object sender, EventArgs e) {
            picBox_StartGame.BackgroundImage = ChatApp.Properties.Resources.gameIconHover;
        }

        private void picBox_StartGame_MouseLeave(object sender, EventArgs e) {
            picBox_StartGame.BackgroundImage = ChatApp.Properties.Resources.gameIcon;
        }

        private void picBox_StartGame_MouseClick(object sender, MouseEventArgs e) {

        }

        private void picBox_EmoteIcon_Click(object sender, EventArgs e) {
            list_Emoticons.Visible = !list_Emoticons.Visible;
            list_Emoticons.Focus();
        }

        private void list_Emoticons_Leave(object sender, EventArgs e) {
            list_Emoticons.Visible = false;
        }

        private void list_Buzzers_Leave(object sender, EventArgs e) {
            list_Buzzers.Visible = false;
        }

        private void picBox_Buzz_Click(object sender, EventArgs e) {
            MouseEventArgs mE = (MouseEventArgs)e;
            if (mE.Button == System.Windows.Forms.MouseButtons.Right) {
                list_Buzzers.Visible = !list_Emoticons.Visible;
                list_Buzzers.Focus();
            } else if (mE.Button == System.Windows.Forms.MouseButtons.Left) {
                //Mandar buzz a los otros parcitipantes
                list_Buzzers.Visible = false;
            }
        }

        private void picBox_CloseIcon_MouseLeave(object sender, EventArgs e) {
            picBox_CloseIcon.BackColor = Color.White;
        }
    }
}
