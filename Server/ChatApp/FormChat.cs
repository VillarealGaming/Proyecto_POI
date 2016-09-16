using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using EasyPOI;
namespace ChatApp
{
    public partial class formChat : Form
    {
        public int chatID { get; set; }
        public List<string> users { get; set; }
        public ListViewItem listItem { get; set; }
        private const int buzzDuration = 500;
        private const int buzzStrength = 5;
        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;
        private Stopwatch buzzStopWatch;
        private Point formStartPoint;
        private List<Point> controlsStartPositions;
        public formChat(int chatID, ListViewItem listItem)
        {
            this.chatID = chatID;
            this.listItem = listItem;
            //buzz
            buzzStopWatch = new Stopwatch();
            formStartPoint = new Point();
            InitializeComponent();
        }

        public void Buzz()
        {
            controlsStartPositions = new List<Point>();
            foreach (Control control in Controls)
                controlsStartPositions.Add(new Point());
            formStartPoint = Location;
            for(int i = 0; i < Controls.Count; i++)
                controlsStartPositions[i] = Controls[i].Location;
            buzzTimer.Start();
            buzzStopWatch.Restart();
        }

        public void CheckEmoticons()
        {
            richTextBoxChat.ReadOnly = false;
            foreach (string[] emoteArray in ClientSession.Emoticons.Values)
            {
                foreach (string emote in emoteArray)
                {
                    while (richTextBoxChat.Text.Contains(emote))
                    {
                        int index = richTextBoxChat.Text.IndexOf(emote);
                        richTextBoxChat.Select(index, emote.Length);
                        Clipboard.SetDataObject(ClientSession.Emoticons.FirstOrDefault(x => x.Value.Contains(emote)).Key, false, 2, 100);
                        richTextBoxChat.Paste();
                    }
                }
            }
            richTextBoxChat.Select(richTextBoxChat.Text.Length, 0);
            richTextBoxChat.ScrollToCaret();
            richTextBoxChat.ReadOnly = true;
        }

        public void AddMessageToChat(string sender, string message)
        {
            richTextBoxChat.SelectionFont = new Font(richTextBoxChat.Font, FontStyle.Bold);
            richTextBoxChat.AppendText(sender + ": ");
            richTextBoxChat.SelectionFont = new Font(richTextBoxChat.Font, FontStyle.Regular);
            richTextBoxChat.AppendText(message + "\n");
        }

        public void SetLastMessage(string sender, string message, DateTime date)
        {
            listItem.SubItems[1].Text = sender + ": " + message;
            if (listItem.SubItems[1].Text.Length > ClientSession.textMessagesVisibleText)
                listItem.SubItems[1].Text = listItem.SubItems[1].Text.Substring(0, ClientSession.textMessagesVisibleText) + "...";
            listItem.SubItems[2].Tag = date;
            listItem.SubItems[2].Text = date.ToString();
        }

        private void buzzTimer_Tick(object sender, EventArgs e)
        {
            if(buzzStopWatch.ElapsedMilliseconds < buzzDuration)
            {
                Location = RandomPoint(formStartPoint, -buzzStrength, buzzStrength);
                for (int i = 0; i < Controls.Count; i++)
                    Controls[i].Location = RandomPoint(controlsStartPositions[i], -buzzStrength, buzzStrength);
            }
            else
            {
                Location = formStartPoint;
                for (int i = 0; i < Controls.Count; i++)
                     Controls[i].Location = controlsStartPositions[i];
                buzzTimer.Stop();
                buzzStopWatch.Reset();
            }
        }

        private Point RandomPoint(Point startPoint, int minValue, int maxValue)
        {
            Random random = new Random();
            return new Point(startPoint.X + random.Next(minValue, maxValue), startPoint.Y + random.Next(minValue, maxValue));
        }

        private void buttonEnviar_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(textBoxChat.Text))
            {
                Packet packet = new Packet(PacketType.TextMessage);
                packet.tag["sender"] = ClientSession.username;
                packet.tag["text"] = textBoxChat.Text;
                packet.tag["chatID"] = chatID;
                packet.tag["date"] = DateTime.Now;
                ClientSession.Connection.SendPacket(packet);
                textBoxChat.Text = "";
            }
        }

        private void formChat_MouseMove(object sender, MouseEventArgs e) {
            if (dragging) {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }
        private void textBoxChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonEnviar.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void formChat_Load(object sender, EventArgs e)
        {
            textBoxChat.Focus();
        }

        private void formChat_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void formChat_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void picBox_CloseIcon_MouseEnter(object sender, EventArgs e)
        {
            picBox_CloseIcon.BackColor = Color.Brown;
        }

        private void picBox_CloseIcon_MouseLeave(object sender, EventArgs e)
        {
            picBox_CloseIcon.BackColor = Color.White;
        }

        private void picBox_CloseIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
        }

        private void picBox_EmoteIcon_MouseEnter(object sender, EventArgs e)
        {
            picBox_EmoteIcon.BackgroundImage = ChatApp.Properties.Resources.emotIconHover;
        }

        private void picBox_EmoteIcon_MouseLeave(object sender, EventArgs e)
        {
            picBox_EmoteIcon.BackgroundImage = ChatApp.Properties.Resources.emotIcon;
        }

        private void picBox_Buzz_MouseEnter(object sender, EventArgs e)
        {
            picBox_Buzz.BackgroundImage = ChatApp.Properties.Resources.buzzIconHover;
        }

        private void picBox_Buzz_MouseLeave(object sender, EventArgs e)
        {
            picBox_Buzz.BackgroundImage = ChatApp.Properties.Resources.buzzIcon;
        }

        private void picBox_Attach_MouseEnter(object sender, EventArgs e)
        {
            picBox_Attach.BackgroundImage = ChatApp.Properties.Resources.attachIconHover;
        }

        private void picBox_Attach_MouseLeave(object sender, EventArgs e)
        {
            picBox_Attach.BackgroundImage = ChatApp.Properties.Resources.attachIcon;
        }
        //unused
        private void picBox_Buzz_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void TextBoxChat_KeyDown_1(object sender, KeyEventArgs e)
        {

        }
        private void picBox_Attach_MouseClick(object sender, MouseEventArgs e) {

        }
        private void picBox_EmoteIcon_MouseClick(object sender, MouseEventArgs e) { }

        private void picBox_Buzz_Click(object sender, EventArgs e)
        {
            MouseEventArgs mE = (MouseEventArgs)e;
            if (mE.Button == System.Windows.Forms.MouseButtons.Left) {
                listViewBuzzers.Visible = false;
                Packet packet = new Packet(PacketType.Buzz);
                packet.tag["sender"] = ClientSession.username;
                packet.tag["chatID"] = chatID;
                ClientSession.Connection.SendPacket(packet);
            } else if (mE.Button == System.Windows.Forms.MouseButtons.Right) {
                listViewBuzzers.Visible = !listViewBuzzers.Visible;
                listViewBuzzers.Focus();
            }
            
        }

        private void listView1_Leave(object sender, EventArgs e)
        {
            listViewEmoticons.Visible = false;
        }

        private void picBox_EmoteIcon_Click(object sender, EventArgs e)
        {
            listViewEmoticons.Visible = !listViewEmoticons.Visible;
            if (listViewEmoticons.Visible)
                listViewEmoticons.Focus();
            else
                textBoxChat.Focus();
        }

        private void listViewEmoticons_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listViewEmoticons_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(listViewEmoticons.SelectedItems.Count == 1)
            {
                listViewEmoticons.Visible = false;
                textBoxChat.AppendText(listViewEmoticons.SelectedItems[0].Tag as string + " ");
                textBoxChat.Focus();
            }
        }

        private void listViewBuzzers_Leave(object sender, EventArgs e) {
            listViewBuzzers.Visible = false;
        }

        private void listViewEmoticons_Leave(object sender, EventArgs e) {
            listViewEmoticons.Visible = false;
        }

        private void SendFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.CheckPathExists = true;
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream fileStream = (FileStream)openFileDialog.OpenFile())
                    {
                        byte[] fileBuffer = new byte[fileStream.Length];
                        fileStream.Read(fileBuffer, 0, (int)fileStream.Length);
                        Packet packet = new Packet(PacketType.FileSendChat);
                        packet.tag["sender"] = ClientSession.username;
                        packet.tag["chatID"] = chatID;
                        packet.tag["filename"] = openFileDialog.FileName;
                        packet.tag["file"] = fileBuffer;
                        ClientSession.Connection.SendPacket(packet);
                    }
                }
            }
        }

        public void ReceiveFile(Packet packet)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "All files (*.*)|*.*";
            saveFileDialog.FileName = packet.tag["filename"] as string;
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    byte[] fileBuffer = (byte[])packet.tag["file"];
                    fileStream.Write(fileBuffer, 0, fileBuffer.Length);
                    fileStream.Flush();
                }
            }
        }
        private void picBox_Attach_Click(object sender, EventArgs e)
        {
            SendFile();
        }

        private void picBox_StartGame_MouseClick(object sender, MouseEventArgs e) { }
    }
}
