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

namespace ChatApp {
    public partial class FormEmail : Form {
        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;

        private AutoCompleteStringCollection autoCompleteUsers;
        public FormEmail() {
            InitializeComponent();
            autoCompleteUsers = new AutoCompleteStringCollection();
            autoCompleteUsers.AddRange(ClientSession.userList.ToArray());
            autoCompleteUsers.Remove(ClientSession.username);
            textBoxTo.AutoCompleteCustomSource = autoCompleteUsers;
            textBoxTo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxTo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            ValidateEnviar();
        }

        private void FormEmail_Load(object sender, EventArgs e) {

        }

        private void ValidateEnviar()
        {
            buttonEnviar.Enabled = !string.IsNullOrWhiteSpace(textBoxSubject.Text);
            buttonEnviar.Enabled = !string.IsNullOrWhiteSpace(richTextBoxBody.Text);
        }

        private void picBox_Attach_MouseEnter(object sender, EventArgs e) {
            picBox_Attach.BackgroundImage = ChatApp.Properties.Resources.attachIconHover;
        }

        private void picBox_Attach_MouseLeave(object sender, EventArgs e) {
            picBox_Attach.BackgroundImage = ChatApp.Properties.Resources.attachIcon;
        }

        private void picBox_Attach_MouseClick(object sender, MouseEventArgs e) {

        }

        private void FormEmail_MouseDown(object sender, MouseEventArgs e) {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void FormEmail_MouseMove(object sender, MouseEventArgs e) {
            if (dragging) {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void FormEmail_MouseUp(object sender, MouseEventArgs e) {
            dragging = false;
        }

        private void picBox_CloseIcon_MouseEnter(object sender, EventArgs e) {
            picBox_CloseIcon.BackColor = Color.Brown;
        }

        private void picBox_CloseIcon_MouseLeave(object sender, EventArgs e) {
            picBox_CloseIcon.BackColor = Color.White;
        }

        private void buttonEnviar_Click(object sender, EventArgs e)
        {
            //Enviar correo
            Packet packet = new Packet(PacketType.SendMail);
            packet.tag["from"] = ClientSession.username;
            packet.tag["to"] = textBoxTo.Text;
            packet.tag["subject"] = textBoxSubject.Text;
            packet.tag["body"] = richTextBoxBody.Text;
            ClientSession.Connection.SendPacket(packet);
            this.Close();
        }

        private void textBoxTo_TextChanged(object sender, EventArgs e)
        {
            buttonEnviar.Enabled = ClientSession.userList.Contains(textBoxTo.Text);
            ValidateEnviar();
        }

        private void richTextBoxBody_TextChanged(object sender, EventArgs e)
        {
            ValidateEnviar();
        }

        private void picBox_CloseIcon_MouseClick(object sender, MouseEventArgs e) {
            this.Close();
        }
    }
}
