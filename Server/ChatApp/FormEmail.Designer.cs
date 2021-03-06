﻿namespace ChatApp {
    partial class FormEmail {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Header = new System.Windows.Forms.Label();
            this.picBox_CloseIcon = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.richTextBoxBody = new System.Windows.Forms.RichTextBox();
            this.buttonEnviar = new System.Windows.Forms.Button();
            this.picBox_Attach = new System.Windows.Forms.PictureBox();
            this.textBoxTo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Attach)).BeginInit();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.Header.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Header.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Header.ForeColor = System.Drawing.Color.White;
            this.Header.Location = new System.Drawing.Point(0, 1);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(383, 24);
            this.Header.TabIndex = 13;
            this.Header.Text = "Enviar Correo Electronico a ...";
            this.Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormEmail_MouseDown);
            this.Header.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormEmail_MouseMove);
            this.Header.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormEmail_MouseUp);
            // 
            // picBox_CloseIcon
            // 
            this.picBox_CloseIcon.BackColor = System.Drawing.Color.White;
            this.picBox_CloseIcon.BackgroundImage = global::ChatApp.Properties.Resources.closeIcon;
            this.picBox_CloseIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_CloseIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_CloseIcon.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBox_CloseIcon.Location = new System.Drawing.Point(357, 1);
            this.picBox_CloseIcon.Name = "picBox_CloseIcon";
            this.picBox_CloseIcon.Size = new System.Drawing.Size(26, 24);
            this.picBox_CloseIcon.TabIndex = 12;
            this.picBox_CloseIcon.TabStop = false;
            this.picBox_CloseIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_CloseIcon_MouseClick);
            this.picBox_CloseIcon.MouseEnter += new System.EventHandler(this.picBox_CloseIcon_MouseEnter);
            this.picBox_CloseIcon.MouseLeave += new System.EventHandler(this.picBox_CloseIcon_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Para";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(9, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Asunto";
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.textBoxSubject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSubject.ForeColor = System.Drawing.Color.White;
            this.textBoxSubject.Location = new System.Drawing.Point(55, 73);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.Size = new System.Drawing.Size(316, 20);
            this.textBoxSubject.TabIndex = 19;
            // 
            // richTextBoxBody
            // 
            this.richTextBoxBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.richTextBoxBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxBody.ForeColor = System.Drawing.Color.White;
            this.richTextBoxBody.Location = new System.Drawing.Point(12, 109);
            this.richTextBoxBody.Name = "richTextBoxBody";
            this.richTextBoxBody.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBoxBody.Size = new System.Drawing.Size(359, 186);
            this.richTextBoxBody.TabIndex = 20;
            this.richTextBoxBody.Text = "";
            this.richTextBoxBody.TextChanged += new System.EventHandler(this.richTextBoxBody_TextChanged);
            // 
            // buttonEnviar
            // 
            this.buttonEnviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.buttonEnviar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEnviar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEnviar.ForeColor = System.Drawing.Color.White;
            this.buttonEnviar.Location = new System.Drawing.Point(296, 304);
            this.buttonEnviar.Name = "buttonEnviar";
            this.buttonEnviar.Size = new System.Drawing.Size(75, 27);
            this.buttonEnviar.TabIndex = 21;
            this.buttonEnviar.Text = "Enviar";
            this.buttonEnviar.UseVisualStyleBackColor = false;
            this.buttonEnviar.Click += new System.EventHandler(this.buttonEnviar_Click);
            // 
            // picBox_Attach
            // 
            this.picBox_Attach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.picBox_Attach.BackgroundImage = global::ChatApp.Properties.Resources.attachIcon;
            this.picBox_Attach.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_Attach.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_Attach.Location = new System.Drawing.Point(12, 304);
            this.picBox_Attach.Name = "picBox_Attach";
            this.picBox_Attach.Size = new System.Drawing.Size(27, 27);
            this.picBox_Attach.TabIndex = 22;
            this.picBox_Attach.TabStop = false;
            this.picBox_Attach.Visible = false;
            this.picBox_Attach.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_Attach_MouseClick);
            this.picBox_Attach.MouseEnter += new System.EventHandler(this.picBox_Attach_MouseEnter);
            this.picBox_Attach.MouseLeave += new System.EventHandler(this.picBox_Attach_MouseLeave);
            // 
            // textBoxTo
            // 
            this.textBoxTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.textBoxTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTo.ForeColor = System.Drawing.Color.White;
            this.textBoxTo.Location = new System.Drawing.Point(55, 40);
            this.textBoxTo.Name = "textBoxTo";
            this.textBoxTo.Size = new System.Drawing.Size(316, 20);
            this.textBoxTo.TabIndex = 23;
            this.textBoxTo.TextChanged += new System.EventHandler(this.textBoxTo_TextChanged);
            // 
            // FormEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(383, 343);
            this.Controls.Add(this.textBoxTo);
            this.Controls.Add(this.picBox_Attach);
            this.Controls.Add(this.buttonEnviar);
            this.Controls.Add(this.richTextBoxBody);
            this.Controls.Add(this.textBoxSubject);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picBox_CloseIcon);
            this.Controls.Add(this.Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEmail";
            this.Text = "FormEmail";
            this.Load += new System.EventHandler(this.FormEmail_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormEmail_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormEmail_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormEmail_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Attach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox_CloseIcon;
        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.RichTextBox richTextBoxBody;
        private System.Windows.Forms.Button buttonEnviar;
        private System.Windows.Forms.PictureBox picBox_Attach;
        private System.Windows.Forms.TextBox textBoxTo;
    }
}