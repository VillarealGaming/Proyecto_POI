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
            this.components = new System.ComponentModel.Container();
            this.buttonCreateChat = new System.Windows.Forms.Button();
            this.textBoxChatName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Header = new System.Windows.Forms.Label();
            this.textBoxUsers = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.quitarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picBox_CloseIcon = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCreateChat
            // 
            this.buttonCreateChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.buttonCreateChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreateChat.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateChat.ForeColor = System.Drawing.Color.White;
            this.buttonCreateChat.Location = new System.Drawing.Point(242, 149);
            this.buttonCreateChat.Name = "buttonCreateChat";
            this.buttonCreateChat.Size = new System.Drawing.Size(75, 23);
            this.buttonCreateChat.TabIndex = 7;
            this.buttonCreateChat.Text = "Crear";
            this.buttonCreateChat.UseVisualStyleBackColor = false;
            this.buttonCreateChat.Click += new System.EventHandler(this.buttonCreateChat_Click);
            // 
            // textBoxChatName
            // 
            this.textBoxChatName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.textBoxChatName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxChatName.ForeColor = System.Drawing.Color.White;
            this.textBoxChatName.Location = new System.Drawing.Point(116, 27);
            this.textBoxChatName.Name = "textBoxChatName";
            this.textBoxChatName.Size = new System.Drawing.Size(202, 20);
            this.textBoxChatName.TabIndex = 10;
            this.textBoxChatName.TextChanged += new System.EventHandler(this.textBoxChatName_TextChanged);
            this.textBoxChatName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxChatName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-1, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Nombre de chat";
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
            // textBoxUsers
            // 
            this.textBoxUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.textBoxUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUsers.ForeColor = System.Drawing.Color.White;
            this.textBoxUsers.Location = new System.Drawing.Point(116, 53);
            this.textBoxUsers.Name = "textBoxUsers";
            this.textBoxUsers.Size = new System.Drawing.Size(202, 20);
            this.textBoxUsers.TabIndex = 16;
            this.textBoxUsers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxUsers_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(-1, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Agregar usuario";
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.listBoxUsers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxUsers.ForeColor = System.Drawing.Color.White;
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.ItemHeight = 15;
            this.listBoxUsers.Location = new System.Drawing.Point(2, 79);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(315, 64);
            this.listBoxUsers.TabIndex = 17;
            this.listBoxUsers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxUsers_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitarToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 26);
            // 
            // quitarToolStripMenuItem
            // 
            this.quitarToolStripMenuItem.Name = "quitarToolStripMenuItem";
            this.quitarToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.quitarToolStripMenuItem.Text = "Quitar";
            this.quitarToolStripMenuItem.Click += new System.EventHandler(this.quitarToolStripMenuItem_Click);
            // 
            // picBox_CloseIcon
            // 
            this.picBox_CloseIcon.BackColor = System.Drawing.Color.White;
            this.picBox_CloseIcon.BackgroundImage = global::ChatApp.Properties.Resources.closeIcon;
            this.picBox_CloseIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_CloseIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_CloseIcon.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBox_CloseIcon.Location = new System.Drawing.Point(295, 0);
            this.picBox_CloseIcon.Name = "picBox_CloseIcon";
            this.picBox_CloseIcon.Size = new System.Drawing.Size(26, 24);
            this.picBox_CloseIcon.TabIndex = 14;
            this.picBox_CloseIcon.TabStop = false;
            this.picBox_CloseIcon.Click += new System.EventHandler(this.picBox_CloseIcon_Click);
            this.picBox_CloseIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_CloseIcon_MouseClick);
            this.picBox_CloseIcon.MouseEnter += new System.EventHandler(this.picBox_CloseIcon_MouseEnter);
            this.picBox_CloseIcon.MouseLeave += new System.EventHandler(this.picBox_CloseIcon_MouseLeave);
            // 
            // FormCreateChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(321, 178);
            this.Controls.Add(this.listBoxUsers);
            this.Controls.Add(this.textBoxUsers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picBox_CloseIcon);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.textBoxChatName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCreateChat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCreateChat";
            this.Text = "FormCreateChat";
            this.Load += new System.EventHandler(this.FormCreateChat_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormCreateChat_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormCreateChat_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormCreateChat_MouseUp);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateChat;
        private System.Windows.Forms.TextBox textBoxChatName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picBox_CloseIcon;
        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.TextBox textBoxUsers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem quitarToolStripMenuItem;
    }
}