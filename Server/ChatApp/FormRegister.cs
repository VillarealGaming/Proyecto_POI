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
    public partial class FormRegister : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;
        public FormRegister()
        {
            InitializeComponent();
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            ClientSession.Connection.OnPacketReceivedFunc(ReceivePacket);
        }

        private void ReceivePacket(Packet packet)
        {
            if (this.InvokeRequired)
            {
                ClientSession.ReceivePacketCallback d = new ClientSession.ReceivePacketCallback(ReceivePacket);
                this.Invoke(d, new object[] { packet });
            }
            else
            {
                //Thread safe code below
                switch (packet.Type)
                {
                    case PacketType.Fail:
                        MessageBox.Show("El nombre de usuario " + packet.tag["message"] + " ya existe", "Error de registro", MessageBoxButtons.OK);
                        break;
                    case PacketType.Register:
                        MessageBox.Show("Usuario " + textBoxUsername.Text + " registrado", "Registro completado", MessageBoxButtons.OK);
                        this.Close();
                        break;
                }
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (comboBoxCarrera.SelectedIndex != -1 &&
                textBoxUsername.Text != "" &&
                textBoxEmail.Text != "" &&
                textBoxPassword.Text != "" &&
                textBoxPasswordConfirm.Text != "")
            {
                if (textBoxPassword.Text == textBoxPasswordConfirm.Text)
                {
                    //ClientSession.Connection.BeginConnect();
                    Packet packet = new Packet(PacketType.Register);
                    packet.tag["password"] = textBoxPassword.Text;
                    packet.tag["username"] = textBoxUsername.Text;
                    packet.tag["carrera"] = comboBoxCarrera.SelectedItem.ToString().Normalize();
                    packet.tag["encrypt"] = checkBoxEncrypt.Checked;
                    ClientSession.Connection.SendPacket(packet);
                }
                else
                {
                    MessageBox.Show("Las contraseñas no coinciden", "Error de registro", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Llena todos los campos", "Error de registro", MessageBoxButtons.OK);
            }
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

        private void FormRegister_MouseDown(object sender, MouseEventArgs e) {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void FormRegister_MouseMove(object sender, MouseEventArgs e) {
            if (dragging) {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void comboBoxCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormRegister_MouseUp(object sender, MouseEventArgs e) {
            dragging = false;
        }
    }
}
