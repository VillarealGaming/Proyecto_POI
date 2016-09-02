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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Comenzar Videollamada");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Enviar Correo");
            this.list_Options = new System.Windows.Forms.ListView();
            this.buttonEnviar = new System.Windows.Forms.Button();
            this.textBoxChat = new System.Windows.Forms.TextBox();
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.Header = new System.Windows.Forms.Label();
            this.picBox_Options = new System.Windows.Forms.PictureBox();
            this.picBox_CloseIcon = new System.Windows.Forms.PictureBox();
            this.picBox_EmoteIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Options)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_EmoteIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // list_Options
            // 
            this.list_Options.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.list_Options.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.list_Options.ForeColor = System.Drawing.Color.White;
            this.list_Options.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.list_Options.Location = new System.Drawing.Point(12, 51);
            this.list_Options.MultiSelect = false;
            this.list_Options.Name = "list_Options";
            this.list_Options.Size = new System.Drawing.Size(121, 46);
            this.list_Options.TabIndex = 16;
            this.list_Options.UseCompatibleStateImageBehavior = false;
            this.list_Options.View = System.Windows.Forms.View.List;
            this.list_Options.Visible = false;
            // 
            // buttonEnviar
            // 
            this.buttonEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.buttonEnviar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEnviar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEnviar.ForeColor = System.Drawing.Color.White;
            this.buttonEnviar.Location = new System.Drawing.Point(244, 362);
            this.buttonEnviar.Name = "buttonEnviar";
            this.buttonEnviar.Size = new System.Drawing.Size(75, 23);
            this.buttonEnviar.TabIndex = 14;
            this.buttonEnviar.Text = "Enviar";
            this.buttonEnviar.UseVisualStyleBackColor = false;
            // 
            // textBoxChat
            // 
            this.textBoxChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
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
            this.richTextBoxChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
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
            this.Header.Text = "Nombre de la Sala";
            this.Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormPrivateChat_MouseDown);
            this.Header.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormPrivateChat_MouseMove);
            this.Header.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormPrivateChat_MouseUp);
            // 
            // picBox_Options
            // 
            this.picBox_Options.BackgroundImage = global::ChatApp.Properties.Resources.optionIcon;
            this.picBox_Options.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_Options.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_Options.Location = new System.Drawing.Point(12, 28);
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
            this.picBox_EmoteIcon.BackgroundImage = global::ChatApp.Properties.Resources.emotIcon;
            this.picBox_EmoteIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_EmoteIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_EmoteIcon.Location = new System.Drawing.Point(214, 362);
            this.picBox_EmoteIcon.Name = "picBox_EmoteIcon";
            this.picBox_EmoteIcon.Size = new System.Drawing.Size(24, 23);
            this.picBox_EmoteIcon.TabIndex = 15;
            this.picBox_EmoteIcon.TabStop = false;
            // 
            // FormPrivateChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(331, 397);
            this.Controls.Add(this.picBox_Options);
            this.Controls.Add(this.picBox_CloseIcon);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.list_Options);
            this.Controls.Add(this.picBox_EmoteIcon);
            this.Controls.Add(this.buttonEnviar);
            this.Controls.Add(this.textBoxChat);
            this.Controls.Add(this.richTextBoxChat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPrivateChat";
            this.Text = "FormPrivateChat";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormPrivateChat_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormPrivateChat_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormPrivateChat_MouseUp);
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
    }
}