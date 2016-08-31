namespace ChatApp
{
    partial class FormHome
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
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Matemáticas");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Física");
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Ciencias Computacionales");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Actuaría");
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Multimedia");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("Seguridad en T.I");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Favoritos");
            this.Header = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.treeViewUsers = new System.Windows.Forms.TreeView();
            this.listViewConversacion = new System.Windows.Forms.ListView();
            this.listViewPrivateMessages = new System.Windows.Forms.ListView();
            this.columnChatName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonNewGroupChat = new System.Windows.Forms.Button();
            this.picBox_CloseIcon = new System.Windows.Forms.PictureBox();
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
            this.Header.Size = new System.Drawing.Size(447, 24);
            this.Header.TabIndex = 11;
            this.Header.Text = "Nombre del contacto";
            this.Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // treeViewUsers
            // 
            this.treeViewUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(150)))), ((int)(((byte)(160)))));
            this.treeViewUsers.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewUsers.ForeColor = System.Drawing.Color.White;
            this.treeViewUsers.Location = new System.Drawing.Point(4, 30);
            this.treeViewUsers.Name = "treeViewUsers";
            treeNode29.Name = "NodeMatematicas";
            treeNode29.Text = "Matemáticas";
            treeNode30.Name = "NodeFisica";
            treeNode30.Text = "Física";
            treeNode31.Name = "NodeLCC";
            treeNode31.Text = "Ciencias Computacionales";
            treeNode32.Name = "NodeActuaria";
            treeNode32.Text = "Actuaría";
            treeNode33.Name = "NodeLMAD";
            treeNode33.Text = "Multimedia";
            treeNode34.Name = "NodeSTI";
            treeNode34.Text = "Seguridad en T.I";
            treeNode35.Name = "NodeFavoritos";
            treeNode35.Text = "Favoritos";
            this.treeViewUsers.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode29,
            treeNode30,
            treeNode31,
            treeNode32,
            treeNode33,
            treeNode34,
            treeNode35});
            this.treeViewUsers.Size = new System.Drawing.Size(169, 382);
            this.treeViewUsers.TabIndex = 14;
            this.treeViewUsers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewUsers_AfterSelect);
            // 
            // listViewConversacion
            // 
            this.listViewConversacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(150)))), ((int)(((byte)(160)))));
            this.listViewConversacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewConversacion.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnChatName,
            this.columnMessage});
            this.listViewConversacion.ForeColor = System.Drawing.Color.White;
            this.listViewConversacion.Location = new System.Drawing.Point(179, 30);
            this.listViewConversacion.MultiSelect = false;
            this.listViewConversacion.Name = "listViewConversacion";
            this.listViewConversacion.Size = new System.Drawing.Size(268, 171);
            this.listViewConversacion.TabIndex = 15;
            this.listViewConversacion.UseCompatibleStateImageBehavior = false;
            this.listViewConversacion.View = System.Windows.Forms.View.Details;
            this.listViewConversacion.SelectedIndexChanged += new System.EventHandler(this.listViewConversacion_SelectedIndexChanged);
            this.listViewConversacion.DoubleClick += new System.EventHandler(this.listViewConversacion_DoubleClick);
            // 
            // listViewPrivateMessages
            // 
            this.listViewPrivateMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(150)))), ((int)(((byte)(160)))));
            this.listViewPrivateMessages.ForeColor = System.Drawing.Color.White;
            this.listViewPrivateMessages.Location = new System.Drawing.Point(179, 241);
            this.listViewPrivateMessages.Name = "listViewPrivateMessages";
            this.listViewPrivateMessages.Size = new System.Drawing.Size(268, 171);
            this.listViewPrivateMessages.TabIndex = 16;
            this.listViewPrivateMessages.UseCompatibleStateImageBehavior = false;
            this.listViewPrivateMessages.View = System.Windows.Forms.View.Details;
            // 
            // columnChatName
            // 
            this.columnChatName.Text = "Chat";
            // 
            // columnMessage
            // 
            this.columnMessage.Text = "Mensaje";
            this.columnMessage.Width = 203;
            // 
            // buttonNewGroupChat
            // 
            this.buttonNewGroupChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.buttonNewGroupChat.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonNewGroupChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNewGroupChat.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewGroupChat.ForeColor = System.Drawing.Color.White;
            this.buttonNewGroupChat.Location = new System.Drawing.Point(372, 207);
            this.buttonNewGroupChat.Name = "buttonNewGroupChat";
            this.buttonNewGroupChat.Size = new System.Drawing.Size(75, 23);
            this.buttonNewGroupChat.TabIndex = 17;
            this.buttonNewGroupChat.Text = "Crear chat";
            this.buttonNewGroupChat.UseVisualStyleBackColor = false;
            this.buttonNewGroupChat.Click += new System.EventHandler(this.buttonNewGroupChat_Click);
            // 
            // picBox_CloseIcon
            // 
            this.picBox_CloseIcon.BackColor = System.Drawing.Color.White;
            this.picBox_CloseIcon.BackgroundImage = global::ChatApp.Properties.Resources.closeIcon;
            this.picBox_CloseIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_CloseIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_CloseIcon.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBox_CloseIcon.Location = new System.Drawing.Point(430, 0);
            this.picBox_CloseIcon.Name = "picBox_CloseIcon";
            this.picBox_CloseIcon.Size = new System.Drawing.Size(26, 24);
            this.picBox_CloseIcon.TabIndex = 10;
            this.picBox_CloseIcon.TabStop = false;
            // 
            // FormHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(455, 424);
            this.Controls.Add(this.buttonNewGroupChat);
            this.Controls.Add(this.listViewPrivateMessages);
            this.Controls.Add(this.listViewConversacion);
            this.Controls.Add(this.treeViewUsers);
            this.Controls.Add(this.picBox_CloseIcon);
            this.Controls.Add(this.Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormHome";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.FormHome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox_CloseIcon;
        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TreeView treeViewUsers;
        private System.Windows.Forms.ListView listViewConversacion;
        private System.Windows.Forms.ListView listViewPrivateMessages;
        private System.Windows.Forms.ColumnHeader columnChatName;
        private System.Windows.Forms.ColumnHeader columnMessage;
        private System.Windows.Forms.Button buttonNewGroupChat;
    }
}