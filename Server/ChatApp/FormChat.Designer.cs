namespace ChatApp
{
    partial class formChat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("", 4);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("", 12);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("", 8);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("", 6);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("", 11);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("", 9);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("", 10);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("", 7);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("", 5);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("", 1);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("", 3);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("", 0);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("", 2);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formChat));
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("", 3);
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("", 0);
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem("", 2);
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.buttonEnviar = new System.Windows.Forms.Button();
            this.Header = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listViewUsers = new System.Windows.Forms.ListView();
            this.picBox_Attach = new System.Windows.Forms.PictureBox();
            this.picBox_Buzz = new System.Windows.Forms.PictureBox();
            this.picBox_CloseIcon = new System.Windows.Forms.PictureBox();
            this.picBox_EmoteIcon = new System.Windows.Forms.PictureBox();
            this.textBoxChat = new System.Windows.Forms.TextBox();
            this.buzzTimer = new System.Windows.Forms.Timer(this.components);
            this.listViewEmoticons = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listViewBuzzers = new System.Windows.Forms.ListView();
            this.checkBoxEncrypt = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Attach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Buzz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_EmoteIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxChat
            // 
            this.richTextBoxChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.richTextBoxChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxChat.CausesValidation = false;
            this.richTextBoxChat.ForeColor = System.Drawing.Color.White;
            this.richTextBoxChat.Location = new System.Drawing.Point(148, 39);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxChat.Size = new System.Drawing.Size(292, 263);
            this.richTextBoxChat.TabIndex = 0;
            this.richTextBoxChat.TabStop = false;
            this.richTextBoxChat.Text = "";
            // 
            // buttonEnviar
            // 
            this.buttonEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.buttonEnviar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEnviar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEnviar.ForeColor = System.Drawing.Color.White;
            this.buttonEnviar.Location = new System.Drawing.Point(365, 371);
            this.buttonEnviar.Name = "buttonEnviar";
            this.buttonEnviar.Size = new System.Drawing.Size(75, 27);
            this.buttonEnviar.TabIndex = 1;
            this.buttonEnviar.Text = "Enviar";
            this.buttonEnviar.UseVisualStyleBackColor = false;
            this.buttonEnviar.Click += new System.EventHandler(this.buttonEnviar_Click);
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.Header.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Header.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Header.ForeColor = System.Drawing.Color.White;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(453, 24);
            this.Header.TabIndex = 3;
            this.Header.Text = "Nombre de la Sala";
            this.Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formChat_MouseDown);
            this.Header.MouseMove += new System.Windows.Forms.MouseEventHandler(this.formChat_MouseMove);
            this.Header.MouseUp += new System.Windows.Forms.MouseEventHandler(this.formChat_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.groupBox1.Controls.Add(this.listViewUsers);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(130, 269);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Participantes";
            // 
            // listViewUsers
            // 
            this.listViewUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.listViewUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewUsers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewUsers.ForeColor = System.Drawing.Color.White;
            this.listViewUsers.Location = new System.Drawing.Point(3, 16);
            this.listViewUsers.MultiSelect = false;
            this.listViewUsers.Name = "listViewUsers";
            this.listViewUsers.Size = new System.Drawing.Size(121, 241);
            this.listViewUsers.TabIndex = 0;
            this.listViewUsers.UseCompatibleStateImageBehavior = false;
            this.listViewUsers.View = System.Windows.Forms.View.List;
            // 
            // picBox_Attach
            // 
            this.picBox_Attach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.picBox_Attach.BackgroundImage = global::ChatApp.Properties.Resources.attachIcon;
            this.picBox_Attach.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_Attach.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_Attach.Location = new System.Drawing.Point(78, 371);
            this.picBox_Attach.Name = "picBox_Attach";
            this.picBox_Attach.Size = new System.Drawing.Size(27, 27);
            this.picBox_Attach.TabIndex = 15;
            this.picBox_Attach.TabStop = false;
            this.picBox_Attach.Click += new System.EventHandler(this.picBox_Attach_Click);
            this.picBox_Attach.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_Attach_MouseClick);
            this.picBox_Attach.MouseEnter += new System.EventHandler(this.picBox_Attach_MouseEnter);
            this.picBox_Attach.MouseLeave += new System.EventHandler(this.picBox_Attach_MouseLeave);
            // 
            // picBox_Buzz
            // 
            this.picBox_Buzz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.picBox_Buzz.BackgroundImage = global::ChatApp.Properties.Resources.buzzIcon;
            this.picBox_Buzz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_Buzz.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_Buzz.Location = new System.Drawing.Point(45, 371);
            this.picBox_Buzz.Name = "picBox_Buzz";
            this.picBox_Buzz.Size = new System.Drawing.Size(27, 27);
            this.picBox_Buzz.TabIndex = 14;
            this.picBox_Buzz.TabStop = false;
            this.picBox_Buzz.Click += new System.EventHandler(this.picBox_Buzz_Click);
            this.picBox_Buzz.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_Buzz_MouseClick);
            this.picBox_Buzz.MouseEnter += new System.EventHandler(this.picBox_Buzz_MouseEnter);
            this.picBox_Buzz.MouseLeave += new System.EventHandler(this.picBox_Buzz_MouseLeave);
            // 
            // picBox_CloseIcon
            // 
            this.picBox_CloseIcon.BackColor = System.Drawing.Color.White;
            this.picBox_CloseIcon.BackgroundImage = global::ChatApp.Properties.Resources.closeIcon;
            this.picBox_CloseIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_CloseIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_CloseIcon.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBox_CloseIcon.Location = new System.Drawing.Point(427, 0);
            this.picBox_CloseIcon.Name = "picBox_CloseIcon";
            this.picBox_CloseIcon.Size = new System.Drawing.Size(26, 24);
            this.picBox_CloseIcon.TabIndex = 12;
            this.picBox_CloseIcon.TabStop = false;
            this.picBox_CloseIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_CloseIcon_MouseClick);
            this.picBox_CloseIcon.MouseEnter += new System.EventHandler(this.picBox_CloseIcon_MouseEnter);
            this.picBox_CloseIcon.MouseLeave += new System.EventHandler(this.picBox_CloseIcon_MouseLeave);
            // 
            // picBox_EmoteIcon
            // 
            this.picBox_EmoteIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.picBox_EmoteIcon.BackgroundImage = global::ChatApp.Properties.Resources.emotIcon;
            this.picBox_EmoteIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_EmoteIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_EmoteIcon.Location = new System.Drawing.Point(12, 371);
            this.picBox_EmoteIcon.Name = "picBox_EmoteIcon";
            this.picBox_EmoteIcon.Size = new System.Drawing.Size(27, 27);
            this.picBox_EmoteIcon.TabIndex = 10;
            this.picBox_EmoteIcon.TabStop = false;
            this.picBox_EmoteIcon.Click += new System.EventHandler(this.picBox_EmoteIcon_Click);
            this.picBox_EmoteIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_EmoteIcon_MouseClick);
            this.picBox_EmoteIcon.MouseEnter += new System.EventHandler(this.picBox_EmoteIcon_MouseEnter);
            this.picBox_EmoteIcon.MouseLeave += new System.EventHandler(this.picBox_EmoteIcon_MouseLeave);
            // 
            // textBoxChat
            // 
            this.textBoxChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.textBoxChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxChat.ForeColor = System.Drawing.Color.White;
            this.textBoxChat.Location = new System.Drawing.Point(12, 308);
            this.textBoxChat.Multiline = true;
            this.textBoxChat.Name = "textBoxChat";
            this.textBoxChat.Size = new System.Drawing.Size(428, 57);
            this.textBoxChat.TabIndex = 0;
            this.textBoxChat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxChat_KeyDown);
            // 
            // buzzTimer
            // 
            this.buzzTimer.Interval = 32;
            this.buzzTimer.Tick += new System.EventHandler(this.buzzTimer_Tick);
            // 
            // listViewEmoticons
            // 
            this.listViewEmoticons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.listViewEmoticons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewEmoticons.ForeColor = System.Drawing.Color.Black;
            this.listViewEmoticons.HoverSelection = true;
            listViewItem1.Tag = ":happy:";
            listViewItem2.Tag = ":wink:";
            listViewItem3.Tag = ":serious:";
            listViewItem4.Tag = ":naughty: ";
            listViewItem5.Tag = ":weird:";
            listViewItem6.Tag = ":smile:";
            listViewItem7.Tag = ":surprise:";
            listViewItem8.Tag = ":sad:";
            listViewItem9.Tag = ":meh:";
            listViewItem10.Tag = ":cool:";
            listViewItem11.Tag = ":dumb:";
            listViewItem12.Tag = ":angry:";
            listViewItem13.Tag = ":devil:";
            this.listViewEmoticons.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13});
            this.listViewEmoticons.LargeImageList = this.imageList1;
            this.listViewEmoticons.Location = new System.Drawing.Point(12, 258);
            this.listViewEmoticons.MultiSelect = false;
            this.listViewEmoticons.Name = "listViewEmoticons";
            this.listViewEmoticons.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.listViewEmoticons.Size = new System.Drawing.Size(124, 107);
            this.listViewEmoticons.SmallImageList = this.imageList1;
            this.listViewEmoticons.TabIndex = 1;
            this.listViewEmoticons.TabStop = false;
            this.listViewEmoticons.TileSize = new System.Drawing.Size(24, 24);
            this.listViewEmoticons.UseCompatibleStateImageBehavior = false;
            this.listViewEmoticons.View = System.Windows.Forms.View.Tile;
            this.listViewEmoticons.Visible = false;
            this.listViewEmoticons.SelectedIndexChanged += new System.EventHandler(this.listViewEmoticons_SelectedIndexChanged);
            this.listViewEmoticons.Leave += new System.EventHandler(this.listViewEmoticons_Leave);
            this.listViewEmoticons.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewEmoticons_MouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "angry.png");
            this.imageList1.Images.SetKeyName(1, "cool.png");
            this.imageList1.Images.SetKeyName(2, "devil.png");
            this.imageList1.Images.SetKeyName(3, "dumb.png");
            this.imageList1.Images.SetKeyName(4, "happy.PNG");
            this.imageList1.Images.SetKeyName(5, "meh.png");
            this.imageList1.Images.SetKeyName(6, "naughty.png");
            this.imageList1.Images.SetKeyName(7, "sad.png");
            this.imageList1.Images.SetKeyName(8, "serious.png");
            this.imageList1.Images.SetKeyName(9, "smile.png");
            this.imageList1.Images.SetKeyName(10, "surprise.png");
            this.imageList1.Images.SetKeyName(11, "weird.png");
            this.imageList1.Images.SetKeyName(12, "wink.png");
            // 
            // listViewBuzzers
            // 
            this.listViewBuzzers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.listViewBuzzers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewBuzzers.ForeColor = System.Drawing.Color.Black;
            this.listViewBuzzers.HoverSelection = true;
            listViewItem14.Tag = ":dumb:";
            listViewItem15.Tag = ":angry:";
            listViewItem16.Tag = ":devil:";
            this.listViewBuzzers.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem14,
            listViewItem15,
            listViewItem16});
            this.listViewBuzzers.LargeImageList = this.imageList1;
            this.listViewBuzzers.Location = new System.Drawing.Point(45, 258);
            this.listViewBuzzers.MultiSelect = false;
            this.listViewBuzzers.Name = "listViewBuzzers";
            this.listViewBuzzers.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.listViewBuzzers.Size = new System.Drawing.Size(124, 107);
            this.listViewBuzzers.SmallImageList = this.imageList1;
            this.listViewBuzzers.TabIndex = 17;
            this.listViewBuzzers.TabStop = false;
            this.listViewBuzzers.TileSize = new System.Drawing.Size(24, 24);
            this.listViewBuzzers.UseCompatibleStateImageBehavior = false;
            this.listViewBuzzers.View = System.Windows.Forms.View.Tile;
            this.listViewBuzzers.Visible = false;
            this.listViewBuzzers.Leave += new System.EventHandler(this.listViewBuzzers_Leave);
            // 
            // checkBoxEncrypt
            // 
            this.checkBoxEncrypt.AutoSize = true;
            this.checkBoxEncrypt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxEncrypt.ForeColor = System.Drawing.Color.White;
            this.checkBoxEncrypt.Location = new System.Drawing.Point(148, 376);
            this.checkBoxEncrypt.Name = "checkBoxEncrypt";
            this.checkBoxEncrypt.Size = new System.Drawing.Size(131, 19);
            this.checkBoxEncrypt.TabIndex = 28;
            this.checkBoxEncrypt.Text = "Habilitar encriptado";
            this.checkBoxEncrypt.UseVisualStyleBackColor = true;
            // 
            // formChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(452, 410);
            this.Controls.Add(this.checkBoxEncrypt);
            this.Controls.Add(this.listViewBuzzers);
            this.Controls.Add(this.listViewEmoticons);
            this.Controls.Add(this.textBoxChat);
            this.Controls.Add(this.picBox_Attach);
            this.Controls.Add(this.picBox_Buzz);
            this.Controls.Add(this.picBox_CloseIcon);
            this.Controls.Add(this.picBox_EmoteIcon);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.buttonEnviar);
            this.Controls.Add(this.richTextBoxChat);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formChat";
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.formChat_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formChat_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.formChat_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.formChat_MouseUp);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Attach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Buzz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_EmoteIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonEnviar;
        private System.Windows.Forms.PictureBox picBox_EmoteIcon;
        public System.Windows.Forms.RichTextBox richTextBoxChat;
        private System.Windows.Forms.PictureBox picBox_CloseIcon;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox picBox_Buzz;
        private System.Windows.Forms.PictureBox picBox_Attach;
        public System.Windows.Forms.ListView listViewUsers;
        public System.Windows.Forms.Label Header;
        private System.Windows.Forms.TextBox textBoxChat;
        private System.Windows.Forms.Timer buzzTimer;
        private System.Windows.Forms.ListView listViewEmoticons;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listViewBuzzers;
        private System.Windows.Forms.CheckBox checkBoxEncrypt;
    }
}

