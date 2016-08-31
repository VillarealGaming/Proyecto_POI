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
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.textBoxChat = new System.Windows.Forms.TextBox();
            this.buttonEnviar = new System.Windows.Forms.Button();
            this.Header = new System.Windows.Forms.Label();
            this.picBox_CloseIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxChat
            // 
            this.richTextBoxChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.richTextBoxChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxChat.ForeColor = System.Drawing.Color.White;
            this.richTextBoxChat.Location = new System.Drawing.Point(13, 35);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ReadOnly = true;
            this.richTextBoxChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxChat.Size = new System.Drawing.Size(259, 288);
            this.richTextBoxChat.TabIndex = 0;
            this.richTextBoxChat.TabStop = false;
            this.richTextBoxChat.Text = "";
            this.richTextBoxChat.TextChanged += new System.EventHandler(this.richTextBoxChat_TextChanged);
            // 
            // textBoxChat
            // 
            this.textBoxChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.textBoxChat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxChat.ForeColor = System.Drawing.Color.White;
            this.textBoxChat.Location = new System.Drawing.Point(13, 343);
            this.textBoxChat.Name = "textBoxChat";
            this.textBoxChat.Size = new System.Drawing.Size(259, 20);
            this.textBoxChat.TabIndex = 1;
            this.textBoxChat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxChat_KeyDown);
            // 
            // buttonEnviar
            // 
            this.buttonEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.buttonEnviar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEnviar.ForeColor = System.Drawing.Color.White;
            this.buttonEnviar.Location = new System.Drawing.Point(197, 371);
            this.buttonEnviar.Name = "buttonEnviar";
            this.buttonEnviar.Size = new System.Drawing.Size(75, 23);
            this.buttonEnviar.TabIndex = 2;
            this.buttonEnviar.Text = "Enviar";
            this.buttonEnviar.UseVisualStyleBackColor = false;
            this.buttonEnviar.Click += new System.EventHandler(this.buttonEnviar_Click);
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.Header.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Header.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Header.ForeColor = System.Drawing.Color.White;
            this.Header.Location = new System.Drawing.Point(1, 1);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(284, 24);
            this.Header.TabIndex = 3;
            this.Header.Text = "Nombre del contacto";
            this.Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formChat_MouseDown);
            this.Header.MouseMove += new System.Windows.Forms.MouseEventHandler(this.formChat_MouseMove);
            this.Header.MouseUp += new System.Windows.Forms.MouseEventHandler(this.formChat_MouseUp);
            // 
            // picBox_CloseIcon
            // 
            this.picBox_CloseIcon.BackColor = System.Drawing.Color.White;
            this.picBox_CloseIcon.BackgroundImage = global::ChatApp.Properties.Resources.closeIcon;
            this.picBox_CloseIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_CloseIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_CloseIcon.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBox_CloseIcon.Location = new System.Drawing.Point(258, 0);
            this.picBox_CloseIcon.Name = "picBox_CloseIcon";
            this.picBox_CloseIcon.Size = new System.Drawing.Size(26, 25);
            this.picBox_CloseIcon.TabIndex = 9;
            this.picBox_CloseIcon.TabStop = false;
            this.picBox_CloseIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_CloseIcon_MouseClick);
            this.picBox_CloseIcon.MouseEnter += new System.EventHandler(this.picBox_CloseIcon_MouseEnter);
            this.picBox_CloseIcon.MouseLeave += new System.EventHandler(this.picBox_CloseIcon_MouseLeave);
            // 
            // formChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.ClientSize = new System.Drawing.Size(284, 406);
            this.Controls.Add(this.picBox_CloseIcon);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.buttonEnviar);
            this.Controls.Add(this.textBoxChat);
            this.Controls.Add(this.richTextBoxChat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formChat";
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.formChat_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formChat_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.formChat_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.formChat_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxChat;
        private System.Windows.Forms.TextBox textBoxChat;
        private System.Windows.Forms.Button buttonEnviar;
        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.PictureBox picBox_CloseIcon;
    }
}

