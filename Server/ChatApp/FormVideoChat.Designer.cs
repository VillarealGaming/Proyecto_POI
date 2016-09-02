namespace ChatApp
{
    partial class FormVideoChat
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
            this.Header = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picBox_EndCall = new System.Windows.Forms.PictureBox();
            this.picBox_Mic = new System.Windows.Forms.PictureBox();
            this.picBox_CloseIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_EndCall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Mic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).BeginInit();
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
            this.Header.Size = new System.Drawing.Size(370, 24);
            this.Header.TabIndex = 6;
            this.Header.Text = "Videollamada";
            this.Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormVideoChat_MouseDown);
            this.Header.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormVideoChat_MouseMove);
            this.Header.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormVideoChat_MouseUp);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(350, 314);
            this.label1.TabIndex = 8;
            // 
            // picBox_EndCall
            // 
            this.picBox_EndCall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.picBox_EndCall.BackgroundImage = global::ChatApp.Properties.Resources.endCallIcon;
            this.picBox_EndCall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_EndCall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_EndCall.Location = new System.Drawing.Point(209, 362);
            this.picBox_EndCall.Name = "picBox_EndCall";
            this.picBox_EndCall.Size = new System.Drawing.Size(42, 40);
            this.picBox_EndCall.TabIndex = 10;
            this.picBox_EndCall.TabStop = false;
            this.picBox_EndCall.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_EndCall_MouseClick);
            this.picBox_EndCall.MouseEnter += new System.EventHandler(this.picBox_EndCall_MouseEnter);
            this.picBox_EndCall.MouseLeave += new System.EventHandler(this.picBox_EndCall_MouseLeave);
            // 
            // picBox_Mic
            // 
            this.picBox_Mic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.picBox_Mic.BackgroundImage = global::ChatApp.Properties.Resources.MicIcon;
            this.picBox_Mic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_Mic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_Mic.Location = new System.Drawing.Point(120, 362);
            this.picBox_Mic.Name = "picBox_Mic";
            this.picBox_Mic.Size = new System.Drawing.Size(42, 40);
            this.picBox_Mic.TabIndex = 9;
            this.picBox_Mic.TabStop = false;
            this.picBox_Mic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_Mic_MouseClick);
            this.picBox_Mic.MouseEnter += new System.EventHandler(this.picBox_Mic_MouseEnter);
            this.picBox_Mic.MouseLeave += new System.EventHandler(this.picBox_Mic_MouseLeave);
            // 
            // picBox_CloseIcon
            // 
            this.picBox_CloseIcon.BackColor = System.Drawing.Color.White;
            this.picBox_CloseIcon.BackgroundImage = global::ChatApp.Properties.Resources.closeIcon;
            this.picBox_CloseIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_CloseIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_CloseIcon.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBox_CloseIcon.Location = new System.Drawing.Point(348, 1);
            this.picBox_CloseIcon.Name = "picBox_CloseIcon";
            this.picBox_CloseIcon.Size = new System.Drawing.Size(26, 24);
            this.picBox_CloseIcon.TabIndex = 7;
            this.picBox_CloseIcon.TabStop = false;
            this.picBox_CloseIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_CloseIcon_MouseClick);
            this.picBox_CloseIcon.MouseEnter += new System.EventHandler(this.picBox_CloseIcon_MouseEnter);
            this.picBox_CloseIcon.MouseLeave += new System.EventHandler(this.picBox_CloseIcon_MouseLeave);
            // 
            // FormVideoChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(374, 414);
            this.Controls.Add(this.picBox_EndCall);
            this.Controls.Add(this.picBox_Mic);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picBox_CloseIcon);
            this.Controls.Add(this.Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormVideoChat";
            this.Text = "FormVideoChat";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormVideoChat_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormVideoChat_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormVideoChat_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_EndCall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Mic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox_CloseIcon;
        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picBox_Mic;
        private System.Windows.Forms.PictureBox picBox_EndCall;
    }
}