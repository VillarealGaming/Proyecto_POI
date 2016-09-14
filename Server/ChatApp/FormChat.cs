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
        //http://csharpdemos.blogspot.mx/2012/10/how-to-insert-smiley-images-in.html
        public static Dictionary<Bitmap, string[]> Emoticons;
        //Emoticons = new Dictionary<Bitmap, string[]>();
        //    Emoticons.Add(ChatApp.Properties.Resources.angry, new string[] { " >:(", ":angry:" });
        //    Emoticons.Add(ChatApp.Properties.Resources.cool, new string[] { " 8)", ":cool:" });
        //    Emoticons.Add(ChatApp.Properties.Resources.devil, new string[] { " >:)", ":devil:" });
        //    Emoticons.Add(ChatApp.Properties.Resources.dumb, new string[] { " :P", ":dumb:" });
        //    Emoticons.Add(ChatApp.Properties.Resources.happy, new string[] { " :)", ":happy:" });
        //    Emoticons.Add(ChatApp.Properties.Resources.meh, new string[] { " :/", ":meh:" });
        //    Emoticons.Add(ChatApp.Properties.Resources.naughty, new string[] { " ^O^", ":naughty:" });
        //    Emoticons.Add(ChatApp.Properties.Resources.sad, new string[] { " :(", ":sad:" });
        //    Emoticons.Add(ChatApp.Properties.Resources.serious, new string[] { " :I", " .-.", " ._.", ":serious:" });
        //    Emoticons.Add(ChatApp.Properties.Resources.smile, new string[] { " :D", ":smile:" });
        //    Emoticons.Add(ChatApp.Properties.Resources.surprise, new string[] { " :O", " :o", " :0", ":surprise:" });
        //    Emoticons.Add(ChatApp.Properties.Resources.weird, new string[] { " :$", " .~.", ":weird:" });
        //    Emoticons.Add(ChatApp.Properties.Resources.wink, new string[] { " ;)", ":wink:" });
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
            foreach (string[] emoteArray in Emoticons.Values)
            {
                foreach (string emote in emoteArray)
                {
                    while (richTextBoxChat.Text.Contains(emote))
                    {
                        int index = richTextBoxChat.Text.IndexOf(emote);
                        richTextBoxChat.Select(index, emote.Length);
                        Clipboard.SetImage(Emoticons.FirstOrDefault(x => x.Value.Contains(emote)).Key);
                        richTextBoxChat.Paste();
                    }
                }
            }
            richTextBoxChat.Select(richTextBoxChat.Text.Length, 0);
            richTextBoxChat.ScrollToCaret();
            richTextBoxChat.ReadOnly = true;
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
            Packet packet = new Packet(PacketType.TextMessage);
            packet.tag["sender"] = ClientSession.username;
            packet.tag["text"] = textBoxChat.Text;
            packet.tag["chatID"] = chatID;
            packet.tag["date"] = DateTime.Now;
            ClientSession.Connection.SendPacket(packet);
            textBoxChat.Text = "";
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

        private void picBox_StartGame_MouseEnter(object sender, EventArgs e)
        {
            picBox_StartGame.BackgroundImage = ChatApp.Properties.Resources.gameIconHover;
        }

        private void picBox_StartGame_MouseLeave(object sender, EventArgs e)
        {
            picBox_StartGame.BackgroundImage = ChatApp.Properties.Resources.gameIcon;
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
            Packet packet = new Packet(PacketType.Buzz);
            packet.tag["sender"] = ClientSession.username;
            packet.tag["chatID"] = chatID;
            ClientSession.Connection.SendPacket(packet);
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

        private void picBox_StartGame_MouseClick(object sender, MouseEventArgs e) { }
    }
}
