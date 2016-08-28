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
        }

        private void formChat_Load(object sender, EventArgs e)
        {
            ClientSession.Connection.OnPacketReceivedFunc(ReceivePacket);
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
    }
}
