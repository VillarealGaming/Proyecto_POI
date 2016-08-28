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
            this.SuspendLayout();
            // 
            // richTextBoxChat
            // 
            this.richTextBoxChat.BackColor = System.Drawing.Color.White;
            this.richTextBoxChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxChat.Location = new System.Drawing.Point(13, 13);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBoxChat.Size = new System.Drawing.Size(259, 288);
            this.richTextBoxChat.TabIndex = 0;
            this.richTextBoxChat.Text = "";
            this.richTextBoxChat.TextChanged += new System.EventHandler(this.richTextBoxChat_TextChanged);
            // 
            // textBoxChat
            // 
            this.textBoxChat.Location = new System.Drawing.Point(13, 321);
            this.textBoxChat.Name = "textBoxChat";
            this.textBoxChat.Size = new System.Drawing.Size(259, 20);
            this.textBoxChat.TabIndex = 1;
            // 
            // buttonEnviar
            // 
            this.buttonEnviar.Location = new System.Drawing.Point(197, 349);
            this.buttonEnviar.Name = "buttonEnviar";
            this.buttonEnviar.Size = new System.Drawing.Size(75, 23);
            this.buttonEnviar.TabIndex = 2;
            this.buttonEnviar.Text = "Enviar";
            this.buttonEnviar.UseVisualStyleBackColor = true;
            this.buttonEnviar.Click += new System.EventHandler(this.buttonEnviar_Click);
            // 
            // formChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 384);
            this.Controls.Add(this.buttonEnviar);
            this.Controls.Add(this.textBoxChat);
            this.Controls.Add(this.richTextBoxChat);
            this.Name = "formChat";
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.formChat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxChat;
        private System.Windows.Forms.TextBox textBoxChat;
        private System.Windows.Forms.Button buttonEnviar;
    }
}

