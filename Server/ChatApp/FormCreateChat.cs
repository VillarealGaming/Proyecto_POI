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
    }
}
