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
        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;
        private AutoCompleteStringCollection autoCompleteUsers;
        public FormCreateChat()
        {
            InitializeComponent();
            autoCompleteUsers = new AutoCompleteStringCollection();
            autoCompleteUsers.AddRange(ClientSession.userList.ToArray());
            autoCompleteUsers.Remove(ClientSession.username);
            textBoxUsers.AutoCompleteCustomSource = autoCompleteUsers;
            textBoxUsers.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxUsers.AutoCompleteSource = AutoCompleteSource.CustomSource;
            ValidateChat();
        }

        private void buttonCreateChat_Click(object sender, EventArgs e)
        {
            if(textBoxChatName.Text.Length != 0)
            {
                Packet packet = new Packet(PacketType.CreatePublicConversation);
                List<string> chatUsers = new List<string>();
                chatUsers = listBoxUsers.Items.Cast<string>().ToList();
                chatUsers.Add(ClientSession.username);
                packet.tag["nombre"] = textBoxChatName.Text;
                packet.tag["users"] = chatUsers;
                ClientSession.Connection.SendPacket(packet);
            }
        }

        private void FormCreateChat_Load(object sender, EventArgs e)
        {
            //ClientSession.Connection.OnPacketReceivedFunc(OnPacket);//
        }

        private void FormCreateChat_MouseDown(object sender, MouseEventArgs e) {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void FormCreateChat_MouseMove(object sender, MouseEventArgs e) {
            if (dragging) {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void FormCreateChat_MouseUp(object sender, MouseEventArgs e) {
            dragging = false;
        }

        private void picBox_CloseIcon_MouseEnter(object sender, EventArgs e) {
            picBox_CloseIcon.BackColor = Color.Brown;
        }

        private void picBox_CloseIcon_MouseLeave(object sender, EventArgs e) {
            picBox_CloseIcon.BackColor = Color.White;
        }

        private void textBoxChatName_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                buttonCreateChat.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void textBoxUsers_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (ClientSession.userList.Contains(textBoxUsers.Text)
                    && !listBoxUsers.Items.Contains(textBoxUsers.Text)
                    && textBoxUsers.Text != ClientSession.username
                    && listBoxUsers.Items.Count == 4)
                {
                    listBoxUsers.Items.Add(textBoxUsers.Text);
                    ValidateChat();
                }
            }
        }
        //Show context menu on listbox item
        //http://stackoverflow.com/questions/376910/how-can-i-add-a-context-menu-to-a-listboxitem
        private void listBoxUsers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var index = listBoxUsers.IndexFromPoint(e.Location);
            if(index != ListBox.NoMatches)
            {
                listBoxUsers.SelectedIndex = index;
                contextMenuStrip1.Show(Cursor.Position);
                contextMenuStrip1.Visible = true;
            }
            else
            {
                contextMenuStrip1.Visible = false;
            }
        }

        private void quitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBoxUsers.Items.RemoveAt(listBoxUsers.SelectedIndex);
            ValidateChat();
        }

        private void picBox_CloseIcon_MouseClick(object sender, MouseEventArgs e) { this.Close(); }
        private void textBoxChatName_TextChanged(object sender, EventArgs e){ ValidateChat(); }

        private void picBox_CloseIcon_Click(object sender, EventArgs e)
        {

        }

        //validamos que existan cuatro usuarios (+1 por el usuario actual)
        private void ValidateChat()
        {
            buttonCreateChat.Enabled = (listBoxUsers.Items.Count == 4 && textBoxChatName.Text != "");
        }
    }
}
