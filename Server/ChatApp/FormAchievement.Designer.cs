namespace ChatApp {
    partial class FormAchievement {
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
            this.lbl_Titulo = new System.Windows.Forms.Label();
            this.lbl_NombreRecurso = new System.Windows.Forms.Label();
            this.lbl_Causa = new System.Windows.Forms.Label();
            this.picBox_ImagenRecurso = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_ImagenRecurso)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Titulo
            // 
            this.lbl_Titulo.AutoSize = true;
            this.lbl_Titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Titulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lbl_Titulo.Location = new System.Drawing.Point(8, 9);
            this.lbl_Titulo.Name = "lbl_Titulo";
            this.lbl_Titulo.Size = new System.Drawing.Size(127, 17);
            this.lbl_Titulo.TabIndex = 0;
            this.lbl_Titulo.Text = "X Desbloqueado";
            // 
            // lbl_NombreRecurso
            // 
            this.lbl_NombreRecurso.AutoSize = true;
            this.lbl_NombreRecurso.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NombreRecurso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lbl_NombreRecurso.Location = new System.Drawing.Point(88, 40);
            this.lbl_NombreRecurso.Name = "lbl_NombreRecurso";
            this.lbl_NombreRecurso.Size = new System.Drawing.Size(141, 26);
            this.lbl_NombreRecurso.TabIndex = 1;
            this.lbl_NombreRecurso.Text = "Nombre de X";
            // 
            // lbl_Causa
            // 
            this.lbl_Causa.AutoSize = true;
            this.lbl_Causa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Causa.ForeColor = System.Drawing.Color.DarkGray;
            this.lbl_Causa.Location = new System.Drawing.Point(89, 91);
            this.lbl_Causa.Name = "lbl_Causa";
            this.lbl_Causa.Size = new System.Drawing.Size(164, 20);
            this.lbl_Causa.TabIndex = 2;
            this.lbl_Causa.Text = "Causa de desbloqueo";
            // 
            // picBox_ImagenRecurso
            // 
            this.picBox_ImagenRecurso.Location = new System.Drawing.Point(13, 41);
            this.picBox_ImagenRecurso.Name = "picBox_ImagenRecurso";
            this.picBox_ImagenRecurso.Size = new System.Drawing.Size(70, 70);
            this.picBox_ImagenRecurso.TabIndex = 3;
            this.picBox_ImagenRecurso.TabStop = false;
            // 
            // FormAchievement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(359, 132);
            this.Controls.Add(this.picBox_ImagenRecurso);
            this.Controls.Add(this.lbl_Causa);
            this.Controls.Add(this.lbl_NombreRecurso);
            this.Controls.Add(this.lbl_Titulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAchievement";
            this.Text = "FormAchievement";
            this.Load += new System.EventHandler(this.FormAchievement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_ImagenRecurso)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Titulo;
        private System.Windows.Forms.Label lbl_NombreRecurso;
        private System.Windows.Forms.Label lbl_Causa;
        private System.Windows.Forms.PictureBox picBox_ImagenRecurso;
    }
}