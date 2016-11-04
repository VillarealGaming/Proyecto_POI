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
//TODO: Agregar un timer de espera máximo para conectarse al servidor
//TODO: Manejar desconexión del servidor
//TODO: Inhabilitar el boton de cerrar del servidor en la consola
namespace ChatApp
{
    public partial class formLogin : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;

        public formLogin()
        {
            InitializeComponent();
            ClientSession.connectionStateHash.Add("Available", new Tuple<int, string>(0, "En linea"));
            ClientSession.connectionStateHash.Add("NotAvailable", new Tuple<int, string>(1, "No disponible"));
            ClientSession.connectionStateHash.Add("Busy", new Tuple<int, string>(2, "Ocupado"));
            ClientSession.connectionStateHash.Add("Offline", new Tuple<int, string>(3, "Desconectado"));
        }
        private void OnPacket(Packet packet)
        {
            if(this.InvokeRequired)
            {
                ClientSession.ReceivePacketCallback d = new ClientSession.ReceivePacketCallback(OnPacket);
                this.Invoke(d, new object[] { packet });
            }
            else
            {
                if(packet.Type == PacketType.SessionBegin)
                {
                    ClientSession.username = textBoxUsername.Text;
                    ClientSession.state = comboBoxEstado.SelectedIndex>0? ((UserConnectionState)comboBoxEstado.SelectedIndex - 1).ToString() : packet.tag["state"] as string;
                    ClientSession.enemiesKilled = (int)packet.tag["enemiesKilled"];
                    this.Hide();
                    FormHome home = new FormHome();
                    home.FormClosed += (s, args) => { this.Close(); };
                    home.OnPacket(new Packet(PacketType.SessionBegin));
                    //formChat chat = new formChat();
                }
                else if(packet.Type == PacketType.Fail)
                {
                    MessageBox.Show(packet.tag["message"] as string, "No se puede iniciar sesión", MessageBoxButtons.OK);
                }
            }
        }
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if(textBoxUsername.Text != "" &&
                textBoxPassword.Text != "")
            {
                Packet sessionBegin = new Packet(PacketType.SessionBegin);
                sessionBegin.tag["username"] = textBoxUsername.Text;
                sessionBegin.tag["password"] = textBoxPassword.Text;
                ClientSession.Connection.SendPacket(sessionBegin);
            }
        }

        private void buttonRegistrar_Click(object sender, EventArgs e)
        {
            FormRegister registerForm = new FormRegister();
            registerForm.ShowDialog();
            ClientSession.Connection.OnPacketReceivedFunc(OnPacket);
        }
        //TODO: Comenzar la conexion en otro form, uno que solo aparezca una vez
        private void formLogin_Load(object sender, EventArgs e)
        {
            comboBoxEstado.SelectedIndex = 0;
        }

        private void formLogin_Shown(object sender, EventArgs e)
        {
            ClientSession.Connection.OnPacketReceivedFunc(OnPacket);
            ClientSession.Connection.BeginConnect();
            int tries = 0;
            while (!ClientSession.Connection.Connected && tries < 100)
            {
                this.Text = "Buscando servidor...";
                tries++;
            }
            if(tries >= 100)
            {
                this.Header.Text = "Servidor no econtrado";
                buttonConnect.Enabled = false;
                buttonRegistrar.Enabled = false;
            }
            this.Text = "Iniciar sesión";
        }

        private void formLogin_MouseMove(object sender, MouseEventArgs e) {
            if (dragging) {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void formLogin_MouseUp(object sender, MouseEventArgs e) {
            dragging = false;
        }

        private void picBox_CloseIcon_MouseEnter(object sender, EventArgs e) {
            picBox_CloseIcon.BackColor = Color.Brown;
        }

        private void picBox_CloseIcon_MouseLeave(object sender, EventArgs e) {
            picBox_CloseIcon.BackColor = Color.White;
        }

        private void picBox_CloseIcon_MouseClick(object sender, MouseEventArgs e) {
            Application.Exit();
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                buttonConnect.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_Registrar_MouseEnter(object sender, EventArgs e) {
            lbl_Registrar.ForeColor = Color.White;
        }

        private void lbl_Registrar_MouseLeave(object sender, EventArgs e) {
            lbl_Registrar.ForeColor = Color.FromArgb(0, 192, 192);
        }

        private void formLogin_MouseDown(object sender, MouseEventArgs e) {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
    }
}
