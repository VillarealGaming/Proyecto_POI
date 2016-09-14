namespace ChatApp
{
    partial class FormPrivateChat
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Comenzar Videollamada"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))));
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Enviar Correo");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrivateChat));
            this.list_Options = new System.Windows.Forms.ListView();
            this.buttonEnviar = new System.Windows.Forms.Button();
            this.textBoxChat = new System.Windows.Forms.TextBox();
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.Header = new System.Windows.Forms.Label();
            this.picBox_StartGame = new System.Windows.Forms.PictureBox();
            this.picBox_Attach = new System.Windows.Forms.PictureBox();
            this.picBox_Buzz = new System.Windows.Forms.PictureBox();
            this.picBox_Options = new System.Windows.Forms.PictureBox();
            this.picBox_CloseIcon = new System.Windows.Forms.PictureBox();
            this.picBox_EmoteIcon = new System.Windows.Forms.PictureBox();
            this.list_Emoticons = new System.Windows.Forms.ListView();
            this.list_Buzzers = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_StartGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Attach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Buzz)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Options)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_EmoteIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // list_Options
            // 
            this.list_Options.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.list_Options.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.list_Options.ForeColor = System.Drawing.Color.White;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            this.list_Options.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.list_Options.Location = new System.Drawing.Point(198, 51);
            this.list_Options.MultiSelect = false;
            this.list_Options.Name = "list_Options";
            this.list_Options.Size = new System.Drawing.Size(121, 49);
            this.list_Options.TabIndex = 16;
            this.list_Options.TileSize = new System.Drawing.Size(1, 1);
            this.list_Options.UseCompatibleStateImageBehavior = false;
            this.list_Options.View = System.Windows.Forms.View.List;
            this.list_Options.Visible = false;
            this.list_Options.SelectedIndexChanged += new System.EventHandler(this.list_Options_SelectedIndexChanged);
            this.list_Options.Leave += new System.EventHandler(this.list_Options_Leave);
            // 
            // buttonEnviar
            // 
            this.buttonEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.buttonEnviar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEnviar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEnviar.ForeColor = System.Drawing.Color.White;
            this.buttonEnviar.Location = new System.Drawing.Point(244, 351);
            this.buttonEnviar.Name = "buttonEnviar";
            this.buttonEnviar.Size = new System.Drawing.Size(75, 27);
            this.buttonEnviar.TabIndex = 14;
            this.buttonEnviar.Text = "Enviar";
            this.buttonEnviar.UseVisualStyleBackColor = false;
            this.buttonEnviar.Click += new System.EventHandler(this.buttonEnviar_Click);
            // 
            // textBoxChat
            // 
            this.textBoxChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.textBoxChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxChat.ForeColor = System.Drawing.Color.White;
            this.textBoxChat.Location = new System.Drawing.Point(12, 305);
            this.textBoxChat.Multiline = true;
            this.textBoxChat.Name = "textBoxChat";
            this.textBoxChat.Size = new System.Drawing.Size(307, 40);
            this.textBoxChat.TabIndex = 13;
            this.textBoxChat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxChat_KeyDown);
            // 
            // richTextBoxChat
            // 
            this.richTextBoxChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.richTextBoxChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxChat.ForeColor = System.Drawing.Color.White;
            this.richTextBoxChat.Location = new System.Drawing.Point(12, 57);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxChat.Size = new System.Drawing.Size(307, 242);
            this.richTextBoxChat.TabIndex = 12;
            this.richTextBoxChat.TabStop = false;
            this.richTextBoxChat.Text = "";
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
            this.Header.TabIndex = 17;
            this.Header.Text = "Nombre del contacto";
            this.Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormPrivateChat_MouseDown);
            this.Header.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormPrivateChat_MouseMove);
            this.Header.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormPrivateChat_MouseUp);
            // 
            // picBox_StartGame
            // 
            this.picBox_StartGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.picBox_StartGame.BackgroundImage = global::ChatApp.Properties.Resources.gameIcon;
            this.picBox_StartGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_StartGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_StartGame.Location = new System.Drawing.Point(198, 351);
            this.picBox_StartGame.Name = "picBox_StartGame";
            this.picBox_StartGame.Size = new System.Drawing.Size(27, 27);
            this.picBox_StartGame.TabIndex = 22;
            this.picBox_StartGame.TabStop = false;
            this.picBox_StartGame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_StartGame_MouseClick);
            this.picBox_StartGame.MouseEnter += new System.EventHandler(this.picBox_StartGame_MouseEnter);
            this.picBox_StartGame.MouseLeave += new System.EventHandler(this.picBox_StartGame_MouseLeave);
            // 
            // picBox_Attach
            // 
            this.picBox_Attach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.picBox_Attach.BackgroundImage = global::ChatApp.Properties.Resources.attachIcon;
            this.picBox_Attach.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_Attach.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_Attach.Location = new System.Drawing.Point(78, 351);
            this.picBox_Attach.Name = "picBox_Attach";
            this.picBox_Attach.Size = new System.Drawing.Size(27, 27);
            this.picBox_Attach.TabIndex = 21;
            this.picBox_Attach.TabStop = false;
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
            this.picBox_Buzz.Location = new System.Drawing.Point(45, 351);
            this.picBox_Buzz.Name = "picBox_Buzz";
            this.picBox_Buzz.Size = new System.Drawing.Size(27, 27);
            this.picBox_Buzz.TabIndex = 20;
            this.picBox_Buzz.TabStop = false;
            this.picBox_Buzz.Click += new System.EventHandler(this.picBox_Buzz_Click);
            this.picBox_Buzz.MouseEnter += new System.EventHandler(this.picBox_Buzz_MouseEnter);
            this.picBox_Buzz.MouseLeave += new System.EventHandler(this.picBox_Buzz_MouseLeave);
            // 
            // picBox_Options
            // 
            this.picBox_Options.BackgroundImage = global::ChatApp.Properties.Resources.optionIcon;
            this.picBox_Options.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_Options.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_Options.Location = new System.Drawing.Point(296, 28);
            this.picBox_Options.Name = "picBox_Options";
            this.picBox_Options.Size = new System.Drawing.Size(23, 23);
            this.picBox_Options.TabIndex = 19;
            this.picBox_Options.TabStop = false;
            this.picBox_Options.Click += new System.EventHandler(this.picBox_Options_Click);
            // 
            // picBox_CloseIcon
            // 
            this.picBox_CloseIcon.BackColor = System.Drawing.Color.White;
            this.picBox_CloseIcon.BackgroundImage = global::ChatApp.Properties.Resources.closeIcon;
            this.picBox_CloseIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_CloseIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_CloseIcon.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBox_CloseIcon.Location = new System.Drawing.Point(307, 0);
            this.picBox_CloseIcon.Name = "picBox_CloseIcon";
            this.picBox_CloseIcon.Size = new System.Drawing.Size(26, 24);
            this.picBox_CloseIcon.TabIndex = 18;
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
            this.picBox_EmoteIcon.Location = new System.Drawing.Point(12, 351);
            this.picBox_EmoteIcon.Name = "picBox_EmoteIcon";
            this.picBox_EmoteIcon.Size = new System.Drawing.Size(27, 27);
            this.picBox_EmoteIcon.TabIndex = 15;
            this.picBox_EmoteIcon.TabStop = false;
            this.picBox_EmoteIcon.Click += new System.EventHandler(this.picBox_EmoteIcon_Click);
            this.picBox_EmoteIcon.MouseEnter += new System.EventHandler(this.picBox_EmoteIcon_MouseEnter);
            this.picBox_EmoteIcon.MouseLeave += new System.EventHandler(this.picBox_EmoteIcon_MouseLeave);
            // 
            // list_Emoticons
            // 
            this.list_Emoticons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.list_Emoticons.ForeColor = System.Drawing.Color.White;
            this.list_Emoticons.Location = new System.Drawing.Point(12, 208);
            this.list_Emoticons.MultiSelect = false;
            this.list_Emoticons.Name = "list_Emoticons";
            this.list_Emoticons.Size = new System.Drawing.Size(163, 143);
            this.list_Emoticons.TabIndex = 23;
            this.list_Emoticons.TileSize = new System.Drawing.Size(1, 1);
            this.list_Emoticons.UseCompatibleStateImageBehavior = false;
            this.list_Emoticons.View = System.Windows.Forms.View.List;
            this.list_Emoticons.Visible = false;
            this.list_Emoticons.Leave += new System.EventHandler(this.list_Emoticons_Leave);
            // 
            // list_Buzzers
            // 
            this.list_Buzzers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.list_Buzzers.ForeColor = System.Drawing.Color.White;
            this.list_Buzzers.Location = new System.Drawing.Point(45, 208);
            this.list_Buzzers.MultiSelect = false;
            this.list_Buzzers.Name = "list_Buzzers";
            this.list_Buzzers.Size = new System.Drawing.Size(163, 143);
            this.list_Buzzers.TabIndex = 24;
            this.list_Buzzers.TileSize = new System.Drawing.Size(1, 1);
            this.list_Buzzers.UseCompatibleStateImageBehavior = false;
            this.list_Buzzers.View = System.Windows.Forms.View.List;
            this.list_Buzzers.Visible = false;
            this.list_Buzzers.Leave += new System.EventHandler(this.list_Buzzers_Leave);
            // 
            // FormPrivateChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(331, 397);
            this.Controls.Add(this.list_Buzzers);
            this.Controls.Add(this.list_Emoticons);
            this.Controls.Add(this.picBox_StartGame);
            this.Controls.Add(this.picBox_Attach);
            this.Controls.Add(this.picBox_Buzz);
            this.Controls.Add(this.picBox_Options);
            this.Controls.Add(this.picBox_CloseIcon);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.list_Options);
            this.Controls.Add(this.picBox_EmoteIcon);
            this.Controls.Add(this.buttonEnviar);
            this.Controls.Add(this.textBoxChat);
            this.Controls.Add(this.richTextBoxChat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPrivateChat";
            this.Text = "FormPrivateChat";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormPrivateChat_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormPrivateChat_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormPrivateChat_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_StartGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Attach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Buzz)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Options)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_EmoteIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView list_Options;
        private System.Windows.Forms.PictureBox picBox_EmoteIcon;
        private System.Windows.Forms.Button buttonEnviar;
        private System.Windows.Forms.TextBox textBoxChat;
        public System.Windows.Forms.RichTextBox richTextBoxChat;
        private System.Windows.Forms.PictureBox picBox_CloseIcon;
        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.PictureBox picBox_Options;
        private System.Windows.Forms.PictureBox picBox_Buzz;
        private System.Windows.Forms.PictureBox picBox_Attach;
        private System.Windows.Forms.PictureBox picBox_StartGame;
        private System.Windows.Forms.ListView list_Emoticons;
        private System.Windows.Forms.ListView list_Buzzers;
    }
}