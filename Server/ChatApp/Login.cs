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
//TODO: Trabajar en las tablas y relaciones de la base de datos
//TODO: Agregar un timer de espera máximo para conectarse al servidor
//TODO: Manejar desconexión del servidor
//TODO: Inhabilitar el boton de cerrar del servidor en la consola
namespace ChatApp
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
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
                if(packet.Content.Type == PacketType.SessionSuccess)
                {
                    ClientSession.username = textBoxUsername.Text;
                    this.Hide();
                    FormHome chat = new FormHome();
                    chat.FormClosed += (s, args) => { this.Close(); };
                    chat.Show();
                }
                else if(packet.Content.Type == PacketType.SessionFail)
                {
                    MessageBox.Show(packet.Content.message, "No se puede iniciar sesión", MessageBoxButtons.OK);
                }
            }
        }
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if(textBoxUsername.Text != "" &&
                textBoxPassword.Text != "")
            {
                SessionBegin sessionBegin = new SessionBegin();
                sessionBegin.message = textBoxUsername.Text;
                sessionBegin.password = textBoxPassword.Text;
                ClientSession.Connection.SendPacket(new Packet(sessionBegin));
            }
        }

        private void buttonRegistrar_Click(object sender, EventArgs e)
        {
            FormRegister registerForm = new FormRegister();
            registerForm.ShowDialog();
        }
        //TODO: Comenzar la conexion en otro form, uno que solo aparezca una vez
        private void formLogin_Load(object sender, EventArgs e)
        {
        }

        private void formLogin_Shown(object sender, EventArgs e)
        {
            ClientSession.Connection.OnPacketReceivedFunc(OnPacket);
            ClientSession.Connection.BeginConnect();
            while (!ClientSession.Connection.Connected)
            {
                this.Text = "Buscando servidor...";
            }
            this.Text = "Iniciar sesión";
        }
    }
}
