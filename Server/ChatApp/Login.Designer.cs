namespace ChatApp
{
    partial class formLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formLogin));
            this.Header = new System.Windows.Forms.Label();
            this.buttonRegistrar = new System.Windows.Forms.Button();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_Registrarte = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.picBox_CloseIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.Header.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Header.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Header.ForeColor = System.Drawing.Color.White;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(370, 24);
            this.Header.TabIndex = 3;
            this.Header.Text = "Bienvenido";
            this.Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formLogin_MouseDown);
            this.Header.MouseMove += new System.Windows.Forms.MouseEventHandler(this.formLogin_MouseMove);
            this.Header.MouseUp += new System.Windows.Forms.MouseEventHandler(this.formLogin_MouseUp);
            // 
            // buttonRegistrar
            // 
            this.buttonRegistrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.buttonRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRegistrar.ForeColor = System.Drawing.Color.White;
            this.buttonRegistrar.Location = new System.Drawing.Point(277, 210);
            this.buttonRegistrar.Name = "buttonRegistrar";
            this.buttonRegistrar.Size = new System.Drawing.Size(75, 23);
            this.buttonRegistrar.TabIndex = 6;
            this.buttonRegistrar.Text = "Regístrarse";
            this.buttonRegistrar.UseVisualStyleBackColor = false;
            this.buttonRegistrar.Visible = false;
            this.buttonRegistrar.Click += new System.EventHandler(this.buttonRegistrar_Click);
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.textBoxUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxUsername.ForeColor = System.Drawing.Color.White;
            this.textBoxUsername.Location = new System.Drawing.Point(137, 120);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(215, 20);
            this.textBoxUsername.TabIndex = 8;
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConnect.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnect.ForeColor = System.Drawing.Color.White;
            this.buttonConnect.Location = new System.Drawing.Point(277, 172);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 11;
            this.buttonConnect.Text = "Entrar";
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nombre de usuario";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.textBoxPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPassword.ForeColor = System.Drawing.Color.White;
            this.textBoxPassword.Location = new System.Drawing.Point(137, 146);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '•';
            this.textBoxPassword.Size = new System.Drawing.Size(215, 20);
            this.textBoxPassword.TabIndex = 9;
            this.textBoxPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPassword_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Contraseña";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(13, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "¿No tienes una cuenta?";
            // 
            // lbl_Registrarte
            // 
            this.lbl_Registrarte.AutoSize = true;
            this.lbl_Registrarte.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Registrarte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lbl_Registrarte.Location = new System.Drawing.Point(148, 176);
            this.lbl_Registrarte.Name = "lbl_Registrarte";
            this.lbl_Registrarte.Size = new System.Drawing.Size(63, 15);
            this.lbl_Registrarte.TabIndex = 13;
            this.lbl_Registrarte.Text = "Regìstrarte";
            this.lbl_Registrarte.Click += new System.EventHandler(this.buttonRegistrar_Click);
            this.lbl_Registrarte.MouseEnter += new System.EventHandler(this.lbl_Registrarte_MouseEnter);
            this.lbl_Registrarte.MouseLeave += new System.EventHandler(this.lbl_Registrarte_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::ChatApp.Properties.Resources.players;
            this.pictureBox1.Location = new System.Drawing.Point(94, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(187, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // picBox_CloseIcon
            // 
            this.picBox_CloseIcon.BackColor = System.Drawing.Color.White;
            this.picBox_CloseIcon.BackgroundImage = global::ChatApp.Properties.Resources.closeIcon;
            this.picBox_CloseIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_CloseIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_CloseIcon.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBox_CloseIcon.Location = new System.Drawing.Point(348, 0);
            this.picBox_CloseIcon.Name = "picBox_CloseIcon";
            this.picBox_CloseIcon.Size = new System.Drawing.Size(26, 24);
            this.picBox_CloseIcon.TabIndex = 5;
            this.picBox_CloseIcon.TabStop = false;
            this.picBox_CloseIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_CloseIcon_MouseClick);
            this.picBox_CloseIcon.MouseEnter += new System.EventHandler(this.picBox_CloseIcon_MouseEnter);
            this.picBox_CloseIcon.MouseLeave += new System.EventHandler(this.picBox_CloseIcon_MouseLeave);
            // 
            // formLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(374, 207);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl_Registrarte);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonRegistrar);
            this.Controls.Add(this.picBox_CloseIcon);
            this.Controls.Add(this.Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formLogin";
            this.Text = "Iniciar sesión";
            this.Load += new System.EventHandler(this.formLogin_Load);
            this.Shown += new System.EventHandler(this.formLogin_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formLogin_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.formLogin_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.formLogin_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.Button buttonRegistrar;
        private System.Windows.Forms.PictureBox picBox_CloseIcon;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_Registrarte;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}