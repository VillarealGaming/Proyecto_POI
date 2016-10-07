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
using System.Threading;

namespace ChatApp
{
    public partial class FormPrivateChat : Form {
        public int chatID { get; set; }
        public ListViewItem listItem { get; set; }
        private const int buzzDuration = 500;
        private const int buzzStrength = 5;

        private const int minSize = 333;
        private const int maxSize = 689;
        private Size resizeRatio = new Size(8, 0);
        private bool resizing = false;
        private bool expanding = true;
        private bool dragging = false;
        private Point dragCursorPoint, dragFormPoint;
        private Stopwatch buzzStopWatch;
        private Point formStartPoint;
        private List<Point> controlsStartPositions;
        private static Mutex clipboardMutex = new Mutex(false, "Clipboard");
        public FormPrivateChat(int chatID, ListViewItem listItem) {
            this.listItem = listItem;
            this.chatID = chatID;
            //buzz
            buzzStopWatch = new Stopwatch();
            formStartPoint = new Point();
            InitializeComponent();
        }

        public void Buzz() {
            controlsStartPositions = new List<Point>();
            foreach (Control control in Controls)
                controlsStartPositions.Add(new Point());
            formStartPoint = Location;
            for (int i = 0; i < Controls.Count; i++)
                controlsStartPositions[i] = Controls[i].Location;
            buzzTimer.Start();
            buzzStopWatch.Restart();
        }

        private void buzzTimer_Tick(object sender, EventArgs e) {
            if (buzzStopWatch.ElapsedMilliseconds < buzzDuration) {
                Location = RandomPoint(formStartPoint, -buzzStrength, buzzStrength);
                for (int i = 0; i < Controls.Count; i++)
                    Controls[i].Location = RandomPoint(controlsStartPositions[i], -buzzStrength, buzzStrength);
            }
            else {
                Location = formStartPoint;
                for (int i = 0; i < Controls.Count; i++)
                    Controls[i].Location = controlsStartPositions[i];
                buzzTimer.Stop();
                buzzStopWatch.Reset();
            }
        }

        private Point RandomPoint(Point startPoint, int minValue, int maxValue) {
            Random random = new Random();
            return new Point(startPoint.X + random.Next(minValue, maxValue), startPoint.Y + random.Next(minValue, maxValue));
        }

        public void CheckEmoticons() {
            richTextBoxChat.ReadOnly = false;
            clipboardMutex.WaitOne();
            foreach (string[] emoteArray in ClientSession.Emoticons.Values) {
                while (true) {
                    foreach (string emote in emoteArray) {
                        try {
                            Clipboard.SetImage(ClientSession.Emoticons.FirstOrDefault(x => x.Value.Contains(emote)).Key);
                            while (richTextBoxChat.Text.Contains(emote)) {
                                int index = richTextBoxChat.Text.IndexOf(emote);
                                richTextBoxChat.Select(index, emote.Length);
                                //Clipboard.SetDataObject(ClientSession.Emoticons.FirstOrDefault(x => x.Value.Contains(emote)).Key, false, 2, 1);
                                richTextBoxChat.Paste();
                            }
                        }
                        catch { }
                    }
                    break;
                }
            }
            clipboardMutex.ReleaseMutex();
            richTextBoxChat.Select(richTextBoxChat.Text.Length, 0);
            richTextBoxChat.ScrollToCaret();
            richTextBoxChat.ReadOnly = true;
        }

        public void AddMessageToChat(string sender, string message) {
            richTextBoxChat.SelectionFont = new Font(richTextBoxChat.Font, FontStyle.Bold);
            richTextBoxChat.AppendText(sender + ": ");
            richTextBoxChat.SelectionFont = new Font(richTextBoxChat.Font, FontStyle.Regular);
            richTextBoxChat.AppendText(message + "\n");
        }

        public void SetLastMessage(string sender, string message, DateTime date) {
            listItem.SubItems[2].Text = message;
            if (listItem.SubItems[2].Text.Length > ClientSession.textMessagesVisibleText)
                listItem.SubItems[2].Text = listItem.SubItems[2].Text.Substring(0, ClientSession.textMessagesVisibleText) + "...";
            listItem.SubItems[1].Tag = date;
            listItem.SubItems[1].Text = date.ToString();
        }

        private void SendFile() {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.CheckPathExists = true;
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    using (FileStream fileStream = (FileStream)openFileDialog.OpenFile()) {
                        byte[] fileBuffer = new byte[fileStream.Length];
                        fileStream.Read(fileBuffer, 0, (int)fileStream.Length);
                        Packet packet = new Packet(PacketType.FileSendPrivate);
                        packet.tag["sender"] = ClientSession.username;
                        packet.tag["chatID"] = chatID;
                        packet.tag["filename"] = openFileDialog.FileName;
                        packet.tag["file"] = fileBuffer;
                        ClientSession.Connection.SendPacket(packet);
                    }
                }
            }
        }

        public void ReceiveFile(Packet packet) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "All files (*.*)|*.*";
            saveFileDialog.FileName = packet.tag["filename"] as string;
            if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create)) {
                    byte[] fileBuffer = (byte[])packet.tag["file"];
                    fileStream.Write(fileBuffer, 0, fileBuffer.Length);
                    fileStream.Flush();
                }
            }
        }

        private void picBox_CloseIcon_MouseClick(object sender, MouseEventArgs e) {
            if (Camera.OwnerChat == chatID) {
                Camera.Stop();
                Microphone.EndRecording();
            }
            this.Hide();
        }

        public void StartWebcam() {
            if (!Camera.IsRunning) {
                Camera.OwnerChat = chatID;
                Camera.Start();
                Camera.OnNewFrameCallback(SendCameraPacket);
                //start audio record
                Microphone.OnAudioInCallback(SendAudioStream);
                Microphone.StartRecording();
            }
        }

        private void list_Options_SelectedIndexChanged(object sender, EventArgs e) {
            //Start camera
            if (list_Options.Items[0].Selected) {
                //If camera is in no use, we can send a webcam request
                if (ClientSession.HasCamera) {
                    if (!Camera.IsRunning) {
                        Packet packet = new Packet(PacketType.WebCamRequest);
                        packet.tag["chatID"] = chatID;
                        packet.tag["sender"] = ClientSession.username;
                        packet.tag["channels"] = Microphone.Channels;
                        ClientSession.Connection.SendPacket(packet);
                        //Camera.OwnerChat = chatID;
                        //Camera.Start();
                        //Camera.OnNewFrameCallback(SendCameraPacket);
                        ////start audio record
                        //Microphone.OnAudioInCallback(SendAudioStream);
                        //Microphone.StartRecording();
                        resizing = true;
                        timer1.Start();
                        picBox_CloseIcon.Location = new Point(665, 0);
                    } else {
                        //Camera is in use;
                        MessageBox.Show("La camara se encuentra en uso por otro chat", "Camara en uso", MessageBoxButtons.OK);
                    }
                }
            }
            list_Options.Visible = false;
            textBoxChat.Focus();
        }

        public void SendAudioStream(byte[] bytes) {
            UdpPacket packet = new UdpPacket(UdpPacketType.AudioStream);
            packet.WriteData(BitConverter.GetBytes(chatID));
            packet.WriteData(BitConverter.GetBytes((int)bytes.Length));
            packet.WriteData(bytes);
            //Get string bytes
            //https://msdn.microsoft.com/en-us/library/ds4kkd55(v=vs.110).aspx
            string username = ClientSession.username;
            byte[] stringBytes = Encoding.Unicode.GetBytes(username);
            //System.Buffer.BlockCopy(username.ToCharArray(), 0, stringBytes, 0, stringBytes.Length);
            packet.WriteData(BitConverter.GetBytes(stringBytes.Length));
            packet.WriteData(stringBytes);
            //wave format

            ClientSession.Connection.SendUdpPacket(packet);
        }
        //camera new frame event
        public void SendCameraPacket(Bitmap bitmap) {
            pictureBoxCamLocal.Image = new Bitmap(bitmap, pictureBoxCamLocal.Size);
            using (Bitmap img = new Bitmap(bitmap, pictureBoxCam.Size)) {
                if (Camera.CanSend) {
                    //pictureBoxCam.Image = img;
                    Packet packet = new Packet(PacketType.WebCamFrame);
                    packet.tag["bitmap"] = img;
                    packet.tag["chatID"] = chatID;
                    packet.tag["sender"] = ClientSession.username;
                    //packet.tag["user"] = Header.Text;
                    ClientSession.Connection.SendPacket(packet);
                    Camera.CanSend = false;
                }
            }

        }

        public void ReceiveCameraPacket(Packet packet) {
            pictureBoxCam.Image = (Bitmap)packet.tag["bitmap"];
        }

        private void buttonEnviar_Click(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(textBoxChat.Text)) {
                Packet packet = new Packet(PacketType.PrivateTextMessage);
                packet.tag["sender"] = ClientSession.username;
                packet.tag["text"] = textBoxChat.Text;
                packet.tag["chatID"] = chatID;
                packet.tag["date"] = DateTime.Now;
                packet.tag["encriptado"] = checkBoxEncrypt.Checked;
                ClientSession.Connection.SendPacket(packet);
                textBoxChat.Text = "";
            }
        }

        private void picBox_EmoteIcon_Click(object sender, EventArgs e) {
            listViewEmoticons.Visible = !listViewEmoticons.Visible;
            if (listViewEmoticons.Visible)
                listViewEmoticons.Focus();
            else
                textBoxChat.Focus();
        }

        private void listViewEmoticons_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewEmoticons.SelectedItems.Count == 1)
            {
                listViewEmoticons.Visible = false;
                textBoxChat.AppendText(listViewEmoticons.SelectedItems[0].Tag as string + " ");
                textBoxChat.Focus();
            }
        }

        private void picBox_Buzz_Click(object sender, EventArgs e)
        {
            Packet packet = new Packet(PacketType.PrivateBuzz);
            packet.tag["sender"] = ClientSession.username;
            packet.tag["chatID"] = chatID;
            ClientSession.Connection.SendPacket(packet);
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

        private void picBox_Options_Click(object sender, EventArgs e)
        {
            list_Options.Visible = !list_Options.Visible;
            if (list_Options.Visible)
            {
                list_Options.Focus();
            }
        }

        private void FormPrivateChat_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void FormPrivateChat_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (resizing) {
                if (expanding) {
                    if (this.Size.Width <= maxSize) {
                        this.Size += resizeRatio;
                    } else {
                        this.Size = new Size(maxSize, 397);
                        expanding = false;
                        resizing = false;
                        timer1.Stop();
                    }
                } else {
                    if (this.Size.Width >= minSize) {
                        this.Size -= resizeRatio;
                    } else {
                        this.Size = new Size(minSize, 397);
                        expanding = true;
                        resizing = false;
                        timer1.Stop();
                    }
                }
            }
        }

        private void list_Options_Leave(object sender, EventArgs e) { list_Options.Visible = false; }
        private void picBox_EmoteIcon_MouseEnter(object sender, EventArgs e) { picBox_EmoteIcon.BackgroundImage = ChatApp.Properties.Resources.emotIconHover; }
        private void picBox_EmoteIcon_MouseLeave(object sender, EventArgs e) { picBox_EmoteIcon.BackgroundImage = ChatApp.Properties.Resources.emotIcon; }
        private void picBox_Buzz_MouseEnter(object sender, EventArgs e) { picBox_Buzz.BackgroundImage = ChatApp.Properties.Resources.buzzIconHover; }
        private void picBox_Buzz_MouseLeave(object sender, EventArgs e) { picBox_Buzz.BackgroundImage = ChatApp.Properties.Resources.buzzIcon; }
        private void picBox_Attach_MouseEnter(object sender, EventArgs e) { picBox_Attach.BackgroundImage = ChatApp.Properties.Resources.attachIconHover; }
        private void picBox_Attach_MouseLeave(object sender, EventArgs e) { picBox_Attach.BackgroundImage = ChatApp.Properties.Resources.attachIcon; }
        private void picBox_Attach_MouseClick(object sender, MouseEventArgs e) { }
        private void picBox_StartGame_MouseEnter(object sender, EventArgs e) { picBox_StartGame.BackgroundImage = ChatApp.Properties.Resources.gameIconHover; }
        private void picBox_StartGame_MouseLeave(object sender, EventArgs e) { picBox_StartGame.BackgroundImage = ChatApp.Properties.Resources.gameIcon; }
        private void picBox_StartGame_MouseClick(object sender, MouseEventArgs e) { }
        private void listViewEmoticons_MouseLeave(object sender, EventArgs e) { listViewEmoticons.Visible = false; }
        private void FormPrivateChat_Load(object sender, EventArgs e) { timer1.Stop(); }
        private void listViewEmoticons_Leave(object sender, EventArgs e) { listViewEmoticons.Visible = false; }
        private void picBox_Attach_Click(object sender, EventArgs e) { SendFile(); }
        private void picBox_CloseIcon_MouseLeave(object sender, EventArgs e) { picBox_CloseIcon.BackColor = Color.White; }
        private void picBox_CloseIcon_MouseEnter(object sender, EventArgs e) { picBox_CloseIcon.BackColor = Color.Brown; }
        private void picBox_CloseIcon_Click(object sender, EventArgs e) { this.Hide(); }

        private void picBox_EndCall_Click(object sender, EventArgs e) {
            resizing = true;
            timer1.Start();
            picBox_CloseIcon.Location = new Point(306, 0);
            if (Camera.OwnerChat == chatID) {
                Camera.Stop();
                Microphone.EndRecording();
            }
        }

        private void FormPrivateChat_MouseUp(object sender, MouseEventArgs e) { dragging = false; }
    }
}
