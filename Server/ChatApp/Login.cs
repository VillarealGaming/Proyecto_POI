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
//TODO: Hacer registro de usuarios
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

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            ClientSession.sessionData.username = textBoxUsername.Text;
            ClientSession.Connection.BeginConnect();
            while(!ClientSession.Connection.Connected)
            {
                this.Text = "Conectando...";
            }
            ClientSession.Connection.SendPacket(new Packet(PacketType.SessionBegin, ClientSession.sessionData));
            //
            this.Hide();
            formChat chat = new formChat();
            chat.FormClosed += (s, args) => this.Close();
            chat.Show();
        }
    }
}
