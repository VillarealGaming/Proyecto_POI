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
            this.SuspendLayout();
            // 
            // buttonCreateChat
            // 
            this.buttonCreateChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.buttonCreateChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreateChat.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateChat.ForeColor = System.Drawing.Color.White;
            this.buttonCreateChat.Location = new System.Drawing.Point(277, 195);
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
            this.checkBoxEncrypt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxEncrypt.ForeColor = System.Drawing.Color.White;
            this.checkBoxEncrypt.Location = new System.Drawing.Point(12, 195);
            this.checkBoxEncrypt.Name = "checkBoxEncrypt";
            this.checkBoxEncrypt.Size = new System.Drawing.Size(82, 17);
            this.checkBoxEncrypt.TabIndex = 8;
            this.checkBoxEncrypt.Text = "Encriptado";
            this.checkBoxEncrypt.UseVisualStyleBackColor = true;
            // 
            // textBoxChatName
            // 
            this.textBoxChatName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxChatName.Location = new System.Drawing.Point(150, 12);
            this.textBoxChatName.Name = "textBoxChatName";
            this.textBoxChatName.Size = new System.Drawing.Size(202, 20);
            this.textBoxChatName.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Nombre de chat";
            // 
            // FormCreateChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(364, 230);
            this.Controls.Add(this.textBoxChatName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxEncrypt);
            this.Controls.Add(this.buttonCreateChat);
            this.Name = "FormCreateChat";
            this.Text = "FormCreateChat";
            this.Load += new System.EventHandler(this.FormCreateChat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateChat;
        private System.Windows.Forms.CheckBox checkBoxEncrypt;
        private System.Windows.Forms.TextBox textBoxChatName;
        private System.Windows.Forms.Label label1;
    }
}