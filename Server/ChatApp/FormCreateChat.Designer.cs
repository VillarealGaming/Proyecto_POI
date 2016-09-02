namespace ChatApp
{
    partial class FormCreateChat
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
            this.buttonCreateChat = new System.Windows.Forms.Button();
            this.checkBoxEncrypt = new System.Windows.Forms.CheckBox();
            this.textBoxChatName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.picBox_CloseIcon = new System.Windows.Forms.PictureBox();
            this.Header = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCreateChat
            // 
            this.buttonCreateChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.buttonCreateChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreateChat.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateChat.ForeColor = System.Drawing.Color.White;
            this.buttonCreateChat.Location = new System.Drawing.Point(253, 120);
            this.buttonCreateChat.Name = "buttonCreateChat";
            this.buttonCreateChat.Size = new System.Drawing.Size(75, 23);
            this.buttonCreateChat.TabIndex = 7;
            this.buttonCreateChat.Text = "Crear";
            this.buttonCreateChat.UseVisualStyleBackColor = false;
            this.buttonCreateChat.Click += new System.EventHandler(this.buttonCreateChat_Click);
            // 
            // checkBoxEncrypt
            // 
            this.checkBoxEncrypt.AutoSize = true;
            this.checkBoxEncrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxEncrypt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxEncrypt.ForeColor = System.Drawing.Color.White;
            this.checkBoxEncrypt.Location = new System.Drawing.Point(12, 123);
            this.checkBoxEncrypt.Name = "checkBoxEncrypt";
            this.checkBoxEncrypt.Size = new System.Drawing.Size(79, 17);
            this.checkBoxEncrypt.TabIndex = 8;
            this.checkBoxEncrypt.Text = "Encriptado";
            this.checkBoxEncrypt.UseVisualStyleBackColor = true;
            // 
            // textBoxChatName
            // 
            this.textBoxChatName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.textBoxChatName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxChatName.ForeColor = System.Drawing.Color.White;
            this.textBoxChatName.Location = new System.Drawing.Point(126, 54);
            this.textBoxChatName.Name = "textBoxChatName";
            this.textBoxChatName.Size = new System.Drawing.Size(202, 20);
            this.textBoxChatName.TabIndex = 10;
            this.textBoxChatName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxChatName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Nombre de chat";
            // 
            // picBox_CloseIcon
            // 
            this.picBox_CloseIcon.BackColor = System.Drawing.Color.White;
            this.picBox_CloseIcon.BackgroundImage = global::ChatApp.Properties.Resources.closeIcon;
            this.picBox_CloseIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_CloseIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_CloseIcon.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBox_CloseIcon.Location = new System.Drawing.Point(322, 0);
            this.picBox_CloseIcon.Name = "picBox_CloseIcon";
            this.picBox_CloseIcon.Size = new System.Drawing.Size(26, 24);
            this.picBox_CloseIcon.TabIndex = 14;
            this.picBox_CloseIcon.TabStop = false;
            this.picBox_CloseIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_CloseIcon_MouseClick);
            this.picBox_CloseIcon.MouseEnter += new System.EventHandler(this.picBox_CloseIcon_MouseEnter);
            this.picBox_CloseIcon.MouseLeave += new System.EventHandler(this.picBox_CloseIcon_MouseLeave);
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.Header.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Header.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Header.ForeColor = System.Drawing.Color.White;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(365, 24);
            this.Header.TabIndex = 13;
            this.Header.Text = "Crear nueva conversacion";
            this.Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormCreateChat_MouseDown);
            this.Header.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormCreateChat_MouseMove);
            this.Header.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormCreateChat_MouseUp);
            // 
            // FormCreateChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(347, 165);
            this.Controls.Add(this.picBox_CloseIcon);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.textBoxChatName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxEncrypt);
            this.Controls.Add(this.buttonCreateChat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCreateChat";
            this.Text = "FormCreateChat";
            this.Load += new System.EventHandler(this.FormCreateChat_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormCreateChat_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormCreateChat_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormCreateChat_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateChat;
        private System.Windows.Forms.CheckBox checkBoxEncrypt;
        private System.Windows.Forms.TextBox textBoxChatName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picBox_CloseIcon;
        private System.Windows.Forms.Label Header;
    }
}