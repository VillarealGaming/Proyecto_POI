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
//TODO: Manejar desconecciones
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

        }
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            ClientSession.username = textBoxUsername.Text;
            SessionBegin sessionBegin = new SessionBegin();
            sessionBegin.message = ClientSession.username;
            sessionBegin.password = textBoxPassword.Text;
            ClientSession.Connection.OnPacketReceivedFunc(OnPacket);
            ClientSession.Connection.SendPacket(new Packet(sessionBegin));
            //
            this.Hide();
            FormHome chat = new FormHome();
            chat.FormClosed += (s, args) => { this.Close(); }; 
            chat.Show();
        }

        private void buttonRegistrar_Click(object sender, EventArgs e)
        {
            FormRegister registerForm = new FormRegister();
            registerForm.ShowDialog();
        }
        //TODO: Comenzar la conexion en otro form, uno que solo aparezca una vez
        private void formLogin_Load(object sender, EventArgs e)
        {
            ClientSession.Connection.BeginConnect();
            while (!ClientSession.Connection.Connected)
            {
                this.Text = "Conectando...";
            }
        }
    }
}
