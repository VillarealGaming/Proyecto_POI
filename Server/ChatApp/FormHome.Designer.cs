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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Matemáticas");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Física");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Ciencias Computacionales");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Actuaría");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Multimedia y Animación Digital");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Seguridad en T.I");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHome));
            this.Header = new System.Windows.Forms.Label();
            this.contextMenuStripEstado = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.estadoDeConexiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conectadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noDisponibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ocupadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desconectadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.treeViewUsers = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listViewConversacion = new System.Windows.Forms.ListView();
            this.columnChatName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnFecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewPrivateMessages = new System.Windows.Forms.ListView();
            this.columnPrivateUser = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPrivateFecha = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPrivateMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonNewGroupChat = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_Jugador = new System.Windows.Forms.Label();
            this.lbl_Rango = new System.Windows.Forms.Label();
            this.picBox_IconoRango = new System.Windows.Forms.PictureBox();
            this.picBox_CloseIcon = new System.Windows.Forms.PictureBox();
            this.contextMenuStripEstado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_IconoRango)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.Header.ContextMenuStrip = this.contextMenuStripEstado;
            this.Header.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Header.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Header.ForeColor = System.Drawing.Color.White;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(636, 24);
            this.Header.TabIndex = 11;
            this.Header.Text = "Nombre del contacto";
            this.Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormHome_MouseDown);
            this.Header.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormHome_MouseMove);
            this.Header.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormHome_MouseUp);
            // 
            // contextMenuStripEstado
            // 
            this.contextMenuStripEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.estadoDeConexiónToolStripMenuItem});
            this.contextMenuStripEstado.Name = "contextMenuStripEstado";
            this.contextMenuStripEstado.Size = new System.Drawing.Size(177, 26);
            // 
            // estadoDeConexiónToolStripMenuItem
            // 
            this.estadoDeConexiónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.conectadoToolStripMenuItem,
            this.noDisponibleToolStripMenuItem,
            this.ocupadoToolStripMenuItem,
            this.desconectadoToolStripMenuItem});
            this.estadoDeConexiónToolStripMenuItem.Name = "estadoDeConexiónToolStripMenuItem";
            this.estadoDeConexiónToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.estadoDeConexiónToolStripMenuItem.Text = "Estado de conexión";
            // 
            // conectadoToolStripMenuItem
            // 
            this.conectadoToolStripMenuItem.Name = "conectadoToolStripMenuItem";
            this.conectadoToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.conectadoToolStripMenuItem.Text = "Conectado";
            this.conectadoToolStripMenuItem.Click += new System.EventHandler(this.conectadoToolStripMenuItem_Click);
            // 
            // noDisponibleToolStripMenuItem
            // 
            this.noDisponibleToolStripMenuItem.Name = "noDisponibleToolStripMenuItem";
            this.noDisponibleToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.noDisponibleToolStripMenuItem.Text = "No disponible";
            this.noDisponibleToolStripMenuItem.Click += new System.EventHandler(this.ocupadoToolStripMenuItem_Click);
            // 
            // ocupadoToolStripMenuItem
            // 
            this.ocupadoToolStripMenuItem.Name = "ocupadoToolStripMenuItem";
            this.ocupadoToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.ocupadoToolStripMenuItem.Text = "Ocupado";
            this.ocupadoToolStripMenuItem.Click += new System.EventHandler(this.ocupadoToolStripMenuItem1_Click);
            // 
            // desconectadoToolStripMenuItem
            // 
            this.desconectadoToolStripMenuItem.Name = "desconectadoToolStripMenuItem";
            this.desconectadoToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.desconectadoToolStripMenuItem.Text = "Desconectado";
            this.desconectadoToolStripMenuItem.Click += new System.EventHandler(this.desconectadoToolStripMenuItem_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // treeViewUsers
            // 
            this.treeViewUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.treeViewUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewUsers.ForeColor = System.Drawing.Color.White;
            this.treeViewUsers.ImageIndex = 0;
            this.treeViewUsers.ImageList = this.imageList1;
            this.treeViewUsers.Location = new System.Drawing.Point(15, 108);
            this.treeViewUsers.Name = "treeViewUsers";
            treeNode1.Name = "NodeMatematicas";
            treeNode1.Text = "Matemáticas";
            treeNode2.Name = "NodeFisica";
            treeNode2.Text = "Física";
            treeNode3.Name = "NodeLCC";
            treeNode3.Text = "Ciencias Computacionales";
            treeNode4.Name = "NodeActuaria";
            treeNode4.Text = "Actuaría";
            treeNode5.Name = "NodeLMAD";
            treeNode5.Text = "Multimedia y Animación Digital";
            treeNode6.Name = "NodeSTI";
            treeNode6.Text = "Seguridad en T.I";
            this.treeViewUsers.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            this.treeViewUsers.SelectedImageIndex = 0;
            this.treeViewUsers.ShowLines = false;
            this.treeViewUsers.ShowPlusMinus = false;
            this.treeViewUsers.ShowRootLines = false;
            this.treeViewUsers.Size = new System.Drawing.Size(204, 406);
            this.treeViewUsers.TabIndex = 14;
            this.treeViewUsers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewUsers_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "iconSelected.png");
            this.imageList1.Images.SetKeyName(1, "iconConnected.png");
            this.imageList1.Images.SetKeyName(2, "iconUnavailable.png");
            this.imageList1.Images.SetKeyName(3, "iconBusy.png");
            this.imageList1.Images.SetKeyName(4, "iconDisconnected.png");
            this.imageList1.Images.SetKeyName(5, "iconRoot.png");
            // 
            // listViewConversacion
            // 
            this.listViewConversacion.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewConversacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.listViewConversacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewConversacion.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnChatName,
            this.columnMessage,
            this.columnFecha});
            this.listViewConversacion.ForeColor = System.Drawing.Color.White;
            this.listViewConversacion.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewConversacion.HotTracking = true;
            this.listViewConversacion.HoverSelection = true;
            this.listViewConversacion.Location = new System.Drawing.Point(225, 108);
            this.listViewConversacion.MultiSelect = false;
            this.listViewConversacion.Name = "listViewConversacion";
            this.listViewConversacion.Size = new System.Drawing.Size(395, 171);
            this.listViewConversacion.TabIndex = 15;
            this.listViewConversacion.UseCompatibleStateImageBehavior = false;
            this.listViewConversacion.View = System.Windows.Forms.View.Details;
            this.listViewConversacion.SelectedIndexChanged += new System.EventHandler(this.listViewConversacion_SelectedIndexChanged);
            this.listViewConversacion.Click += new System.EventHandler(this.listViewConversacion_Click);
            // 
            // columnChatName
            // 
            this.columnChatName.Text = "Chat";
            this.columnChatName.Width = 75;
            // 
            // columnMessage
            // 
            this.columnMessage.DisplayIndex = 2;
            this.columnMessage.Text = "Mensaje";
            this.columnMessage.Width = 178;
            // 
            // columnFecha
            // 
            this.columnFecha.DisplayIndex = 1;
            this.columnFecha.Text = "Fecha";
            this.columnFecha.Width = 137;
            // 
            // listViewPrivateMessages
            // 
            this.listViewPrivateMessages.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listViewPrivateMessages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(53)))), ((int)(((byte)(60)))));
            this.listViewPrivateMessages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewPrivateMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPrivateUser,
            this.columnPrivateFecha,
            this.columnPrivateMessage});
            this.listViewPrivateMessages.ForeColor = System.Drawing.Color.White;
            this.listViewPrivateMessages.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewPrivateMessages.HotTracking = true;
            this.listViewPrivateMessages.HoverSelection = true;
            this.listViewPrivateMessages.Location = new System.Drawing.Point(225, 314);
            this.listViewPrivateMessages.MultiSelect = false;
            this.listViewPrivateMessages.Name = "listViewPrivateMessages";
            this.listViewPrivateMessages.Size = new System.Drawing.Size(395, 171);
            this.listViewPrivateMessages.TabIndex = 16;
            this.listViewPrivateMessages.UseCompatibleStateImageBehavior = false;
            this.listViewPrivateMessages.View = System.Windows.Forms.View.Details;
            this.listViewPrivateMessages.Click += new System.EventHandler(this.listViewPrivateMessages_Click);
            // 
            // columnPrivateUser
            // 
            this.columnPrivateUser.Text = "Usuario";
            this.columnPrivateUser.Width = 80;
            // 
            // columnPrivateFecha
            // 
            this.columnPrivateFecha.Text = "Fecha";
            this.columnPrivateFecha.Width = 139;
            // 
            // columnPrivateMessage
            // 
            this.columnPrivateMessage.Text = "Mensaje";
            this.columnPrivateMessage.Width = 171;
            // 
            // buttonNewGroupChat
            // 
            this.buttonNewGroupChat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.buttonNewGroupChat.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonNewGroupChat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNewGroupChat.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewGroupChat.ForeColor = System.Drawing.Color.White;
            this.buttonNewGroupChat.Location = new System.Drawing.Point(545, 285);
            this.buttonNewGroupChat.Name = "buttonNewGroupChat";
            this.buttonNewGroupChat.Size = new System.Drawing.Size(75, 23);
            this.buttonNewGroupChat.TabIndex = 17;
            this.buttonNewGroupChat.Text = "Crear chat";
            this.buttonNewGroupChat.UseVisualStyleBackColor = false;
            this.buttonNewGroupChat.Click += new System.EventHandler(this.buttonNewGroupChat_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(172)))), ((int)(((byte)(165)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(545, 491);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Mensaje";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_Jugador
            // 
            this.lbl_Jugador.AutoSize = true;
            this.lbl_Jugador.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Jugador.ForeColor = System.Drawing.Color.White;
            this.lbl_Jugador.Location = new System.Drawing.Point(81, 42);
            this.lbl_Jugador.Name = "lbl_Jugador";
            this.lbl_Jugador.Size = new System.Drawing.Size(235, 29);
            this.lbl_Jugador.TabIndex = 19;
            this.lbl_Jugador.Text = "Nombre del Jugador";
            // 
            // lbl_Rango
            // 
            this.lbl_Rango.AutoSize = true;
            this.lbl_Rango.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Rango.ForeColor = System.Drawing.Color.Silver;
            this.lbl_Rango.Location = new System.Drawing.Point(82, 82);
            this.lbl_Rango.Name = "lbl_Rango";
            this.lbl_Rango.Size = new System.Drawing.Size(92, 20);
            this.lbl_Rango.TabIndex = 21;
            this.lbl_Rango.Text = "Titulo rango";
            // 
            // picBox_IconoRango
            // 
            this.picBox_IconoRango.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_IconoRango.Location = new System.Drawing.Point(15, 42);
            this.picBox_IconoRango.Name = "picBox_IconoRango";
            this.picBox_IconoRango.Size = new System.Drawing.Size(60, 60);
            this.picBox_IconoRango.TabIndex = 20;
            this.picBox_IconoRango.TabStop = false;
            // 
            // picBox_CloseIcon
            // 
            this.picBox_CloseIcon.BackColor = System.Drawing.Color.White;
            this.picBox_CloseIcon.BackgroundImage = global::ChatApp.Properties.Resources.closeIcon;
            this.picBox_CloseIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBox_CloseIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox_CloseIcon.Cursor = System.Windows.Forms.Cursors.Default;
            this.picBox_CloseIcon.Location = new System.Drawing.Point(607, 0);
            this.picBox_CloseIcon.Name = "picBox_CloseIcon";
            this.picBox_CloseIcon.Size = new System.Drawing.Size(26, 24);
            this.picBox_CloseIcon.TabIndex = 10;
            this.picBox_CloseIcon.TabStop = false;
            this.picBox_CloseIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picBox_CloseIcon_MouseClick);
            this.picBox_CloseIcon.MouseEnter += new System.EventHandler(this.picBox_CloseIcon_MouseEnter);
            this.picBox_CloseIcon.MouseLeave += new System.EventHandler(this.picBox_CloseIcon_MouseLeave);
            // 
            // FormHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(65)))), ((int)(((byte)(75)))));
            this.ClientSize = new System.Drawing.Size(632, 524);
            this.Controls.Add(this.lbl_Rango);
            this.Controls.Add(this.picBox_IconoRango);
            this.Controls.Add(this.lbl_Jugador);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonNewGroupChat);
            this.Controls.Add(this.listViewPrivateMessages);
            this.Controls.Add(this.listViewConversacion);
            this.Controls.Add(this.treeViewUsers);
            this.Controls.Add(this.picBox_CloseIcon);
            this.Controls.Add(this.Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormHome";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.FormHome_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormHome_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormHome_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormHome_MouseUp);
            this.contextMenuStripEstado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_IconoRango)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_CloseIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.PictureBox picBox_CloseIcon;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TreeView treeViewUsers;
        private System.Windows.Forms.ListView listViewConversacion;
        private System.Windows.Forms.ListView listViewPrivateMessages;
        private System.Windows.Forms.ColumnHeader columnChatName;
        private System.Windows.Forms.ColumnHeader columnMessage;
        private System.Windows.Forms.Button buttonNewGroupChat;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripEstado;
        private System.Windows.Forms.ToolStripMenuItem estadoDeConexiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conectadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noDisponibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ocupadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desconectadoToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader columnFecha;
        private System.Windows.Forms.Label lbl_Jugador;
        private System.Windows.Forms.PictureBox picBox_IconoRango;
        private System.Windows.Forms.Label lbl_Rango;
        private System.Windows.Forms.ColumnHeader columnPrivateUser;
        private System.Windows.Forms.ColumnHeader columnPrivateFecha;
        private System.Windows.Forms.ColumnHeader columnPrivateMessage;
    }
}