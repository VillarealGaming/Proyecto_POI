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
                switch (packet.Content.Type)
                {
                    case PacketType.RegisterFail:
                        MessageBox.Show("El nombre de usuario " + packet.Content.message + " ya existe", "Error de registro", MessageBoxButtons.OK);
                        break;
                    case PacketType.RegisterSucessfull:
                        MessageBox.Show("Usuario " + packet.Content.message + " registrado", "Registro completado", MessageBoxButtons.OK);
                        this.Close();
                        break;
                }
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (comboBoxCarrera.SelectedIndex != -1 &&
                textBoxUsername.Text != "" &&
                textBoxPassword.Text != "" &&
                textBoxPasswordConfirm.Text != "")
            {
                if (textBoxPassword.Text == textBoxPasswordConfirm.Text)
                {
                    //ClientSession.Connection.BeginConnect();
                    RegisterPacket packet = new RegisterPacket();
                    packet.password = textBoxPassword.Text;
                    packet.message = textBoxUsername.Text;
                    packet.carrera = comboBoxCarrera.SelectedItem.ToString().Normalize();
                    packet.encrypt = checkBoxEncrypt.Checked;
                    ClientSession.Connection.SendPacket(new Packet(packet));
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
        //Unused events
        private void label1_Click(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
    }
}
