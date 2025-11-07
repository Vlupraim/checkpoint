namespace checkpoint
{
    partial class FrmPrincipal
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnUsuarios = new System.Windows.Forms.Button(); // NUEVO BOTÓN
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnSedesUbicaciones = new System.Windows.Forms.Button();
            this.btnMovimientos = new System.Windows.Forms.Button();
            this.btnLotes = new System.Windows.Forms.Button();
            this.btnProductos = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.chkLightTheme = new System.Windows.Forms.CheckBox(); // THEME TOGGLE
            this.labelTitle = new System.Windows.Forms.Label();
            this.pictureBoxUser = new System.Windows.Forms.PictureBox();
            this.panelContent = new System.Windows.Forms.Panel();
            this.flowMetrics = new System.Windows.Forms.FlowLayoutPanel();
            this.panelTileAlerts = new System.Windows.Forms.Panel();
            this.lblAlertsTitle = new System.Windows.Forms.Label();
            this.lblAlertsValue = new System.Windows.Forms.Label();
            this.tableMain = new System.Windows.Forms.TableLayoutPanel();
            this.groupChart = new System.Windows.Forms.GroupBox();
            this.pictureBoxChart = new System.Windows.Forms.PictureBox();
            this.groupRight = new System.Windows.Forms.GroupBox();
            this.panelQuickActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnQuick1 = new System.Windows.Forms.Button();
            this.btnQuick2 = new System.Windows.Forms.Button();
            this.btnQuick3 = new System.Windows.Forms.Button();
            this.btnQuick4 = new System.Windows.Forms.Button();
            this.btnTestDb = new System.Windows.Forms.Button();
            this.panelSidebar.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUser)).BeginInit();
            this.panelContent.SuspendLayout();
            this.flowMetrics.SuspendLayout();
            this.panelTileAlerts.SuspendLayout();
            this.tableMain.SuspendLayout();
            this.groupChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxChart)).BeginInit();
            this.groupRight.SuspendLayout();
            this.panelQuickActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))), ((int)(((byte)(255)))));
            this.panelSidebar.Controls.Add(this.btnUsuarios); // AÑADIDO
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Controls.Add(this.btnSettings);
            this.panelSidebar.Controls.Add(this.btnReportes);
            this.panelSidebar.Controls.Add(this.btnSedesUbicaciones);
            this.panelSidebar.Controls.Add(this.btnMovimientos);
            this.panelSidebar.Controls.Add(this.btnLotes);
            this.panelSidebar.Controls.Add(this.btnProductos);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(300, 923);
            this.panelSidebar.TabIndex = 0;
            // 
            // btnUsuarios (NUEVO)
            // 
            this.btnUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnUsuarios.Location = new System.Drawing.Point(15, 388); // Nueva posición
            this.btnUsuarios.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnUsuarios.Name = "btnUsuarios";
            this.btnUsuarios.Size = new System.Drawing.Size(270, 55);
            this.btnUsuarios.TabIndex = 8; // Nuevo TabIndex
            this.btnUsuarios.Text = "Gestionar Usuarios";
            this.btnUsuarios.UseVisualStyleBackColor = true;
            this.btnUsuarios.Click += new System.EventHandler(this.btnUsuarios_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(15, 850); // Posición actualizada
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(270, 55);
            this.btnLogout.TabIndex = 7;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(15, 773); // Posición actualizada
            this.btnSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(270, 55);
            this.btnSettings.TabIndex = 6;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // btnReportes
            // 
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.ForeColor = System.Drawing.Color.White;
            this.btnReportes.Location = new System.Drawing.Point(15, 323);
            this.btnReportes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(270, 55);
            this.btnReportes.TabIndex = 5;
            this.btnReportes.Text = "Reportes";
            this.btnReportes.UseVisualStyleBackColor = true;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // btnSedesUbicaciones
            // 
            this.btnSedesUbicaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSedesUbicaciones.ForeColor = System.Drawing.Color.White;
            this.btnSedesUbicaciones.Location = new System.Drawing.Point(15, 258);
            this.btnSedesUbicaciones.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSedesUbicaciones.Name = "btnSedesUbicaciones";
            this.btnSedesUbicaciones.Size = new System.Drawing.Size(270, 55);
            this.btnSedesUbicaciones.TabIndex = 4;
            this.btnSedesUbicaciones.Text = "Sedes / Ubicaciones";
            this.btnSedesUbicaciones.UseVisualStyleBackColor = true;
            this.btnSedesUbicaciones.Click += new System.EventHandler(this.btnSedesUbicaciones_Click);
            // 
            // btnMovimientos
            // 
            this.btnMovimientos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMovimientos.ForeColor = System.Drawing.Color.White;
            this.btnMovimientos.Location = new System.Drawing.Point(15, 194);
            this.btnMovimientos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMovimientos.Name = "btnMovimientos";
            this.btnMovimientos.Size = new System.Drawing.Size(270, 55);
            this.btnMovimientos.TabIndex = 3;
            this.btnMovimientos.Text = "Movimientos";
            this.btnMovimientos.UseVisualStyleBackColor = true;
            this.btnMovimientos.Click += new System.EventHandler(this.btnMovimientos_Click);
            // 
            // btnLotes
            // 
            this.btnLotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLotes.ForeColor = System.Drawing.Color.White;
            this.btnLotes.Location = new System.Drawing.Point(15, 129);
            this.btnLotes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLotes.Name = "btnLotes";
            this.btnLotes.Size = new System.Drawing.Size(270, 55);
            this.btnLotes.TabIndex = 2;
            this.btnLotes.Text = "Lotes";
            this.btnLotes.UseVisualStyleBackColor = true;
            this.btnLotes.Click += new System.EventHandler(this.btnLotes_Click);
            // 
            // btnProductos
            // 
            this.btnProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProductos.ForeColor = System.Drawing.Color.White;
            this.btnProductos.Location = new System.Drawing.Point(15, 65);
            this.btnProductos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(270, 55);
            this.btnProductos.TabIndex = 1;
            this.btnProductos.Text = "Productos";
            this.btnProductos.UseVisualStyleBackColor = true;
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(15, 9);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(270, 46);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = true;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(104)))), ((int)(((byte)(206)))));
            this.panelTop.Controls.Add(this.chkLightTheme);
            this.panelTop.Controls.Add(this.labelTitle);
            this.panelTop.Controls.Add(this.pictureBoxUser);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(300, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1350, 92);
            this.panelTop.TabIndex = 1;
            // 
            // chkLightTheme
            // 
            this.chkLightTheme.AutoSize = true;
            this.chkLightTheme.ForeColor = System.Drawing.Color.White;
            this.chkLightTheme.Location = new System.Drawing.Point(1120, 34);
            this.chkLightTheme.Name = "chkLightTheme";
            this.chkLightTheme.Size = new System.Drawing.Size(120, 24);
            this.chkLightTheme.TabIndex = 2;
            this.chkLightTheme.Text = "Modo claro";
            this.chkLightTheme.UseVisualStyleBackColor = true;
            this.chkLightTheme.CheckedChanged += new System.EventHandler(this.chkLightTheme_CheckedChanged);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(21, 28);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(221, 32);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Admin Dashboard";
            // 
            // pictureBoxUser
            // 
            this.pictureBoxUser.Location = new System.Drawing.Point(1260, 15);
            this.pictureBoxUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBoxUser.Name = "pictureBoxUser";
            this.pictureBoxUser.Size = new System.Drawing.Size(60, 62);
            this.pictureBoxUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxUser.TabIndex = 1;
            this.pictureBoxUser.TabStop = false;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelContent.Controls.Add(this.flowMetrics);
            this.panelContent.Controls.Add(this.tableMain);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(300, 92);
            this.panelContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1350, 831);
            this.panelContent.TabIndex = 2;
            // 
            // flowMetrics
            // 
            this.flowMetrics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowMetrics.BackColor = System.Drawing.Color.Transparent;
            this.flowMetrics.Controls.Add(this.panelTileAlerts);
            this.flowMetrics.Location = new System.Drawing.Point(21, 22);
            this.flowMetrics.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowMetrics.Name = "flowMetrics";
            this.flowMetrics.Size = new System.Drawing.Size(1305, 169);
            this.flowMetrics.TabIndex = 0;
            // 
            // panelTileAlerts
            // 
            this.panelTileAlerts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.panelTileAlerts.Controls.Add(this.lblAlertsTitle);
            this.panelTileAlerts.Controls.Add(this.lblAlertsValue);
            this.panelTileAlerts.Location = new System.Drawing.Point(9, 9);
            this.panelTileAlerts.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
            this.panelTileAlerts.Name = "panelTileAlerts";
            this.panelTileAlerts.Size = new System.Drawing.Size(300, 138);
            this.panelTileAlerts.TabIndex = 3;
            // 
            // lblAlertsTitle
            // 
            this.lblAlertsTitle.AutoSize = true;
            this.lblAlertsTitle.ForeColor = System.Drawing.Color.White;
            this.lblAlertsTitle.Location = new System.Drawing.Point(18, 15);
            this.lblAlertsTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAlertsTitle.Name = "lblAlertsTitle";
            this.lblAlertsTitle.Size = new System.Drawing.Size(83, 20);
            this.lblAlertsTitle.TabIndex = 0;
            this.lblAlertsTitle.Text = "ALERTAS";
            this.lblAlertsTitle.Click += new System.EventHandler(this.lblAlertsTitle_Click);
            // 
            // lblAlertsValue
            // 
            this.lblAlertsValue.AutoSize = true;
            this.lblAlertsValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblAlertsValue.ForeColor = System.Drawing.Color.White;
            this.lblAlertsValue.Location = new System.Drawing.Point(18, 46);
            this.lblAlertsValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAlertsValue.Name = "lblAlertsValue";
            this.lblAlertsValue.Size = new System.Drawing.Size(0, 48);
            this.lblAlertsValue.TabIndex = 1;
            // 
            // tableMain
            // 
            this.tableMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableMain.ColumnCount = 2;
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableMain.Controls.Add(this.groupChart, 0, 0);
            this.tableMain.Controls.Add(this.groupRight, 1, 0);
            this.tableMain.Location = new System.Drawing.Point(21, 215);
            this.tableMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableMain.Name = "tableMain";
            this.tableMain.RowCount = 1;
            this.tableMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableMain.Size = new System.Drawing.Size(1305, 585);
            this.tableMain.TabIndex = 1;
            // 
            // groupChart
            // 
            this.groupChart.Controls.Add(this.pictureBoxChart);
            this.groupChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupChart.Location = new System.Drawing.Point(4, 5);
            this.groupChart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupChart.Name = "groupChart";
            this.groupChart.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupChart.Size = new System.Drawing.Size(905, 575);
            this.groupChart.TabIndex = 0;
            this.groupChart.TabStop = false;
            this.groupChart.Text = "Overview";
            // 
            // pictureBoxChart
            // 
            this.pictureBoxChart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBoxChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxChart.Location = new System.Drawing.Point(4, 24);
            this.pictureBoxChart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBoxChart.Name = "pictureBoxChart";
            this.pictureBoxChart.Size = new System.Drawing.Size(897, 546);
            this.pictureBoxChart.TabIndex = 0;
            this.pictureBoxChart.TabStop = false;
            // 
            // groupRight
            // 
            this.groupRight.Controls.Add(this.panelQuickActions);
            this.groupRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupRight.Location = new System.Drawing.Point(917, 5);
            this.groupRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupRight.Name = "groupRight";
            this.groupRight.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupRight.Size = new System.Drawing.Size(384, 575);
            this.groupRight.TabIndex = 1;
            this.groupRight.TabStop = false;
            this.groupRight.Text = "Quick Actions";
            // 
            // panelQuickActions
            // 
            this.panelQuickActions.AutoScroll = true;
            this.panelQuickActions.Controls.Add(this.btnQuick1);
            this.panelQuickActions.Controls.Add(this.btnQuick2);
            this.panelQuickActions.Controls.Add(this.btnQuick3);
            this.panelQuickActions.Controls.Add(this.btnQuick4);
            this.panelQuickActions.Controls.Add(this.btnTestDb);
            this.panelQuickActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelQuickActions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelQuickActions.Location = new System.Drawing.Point(4, 24);
            this.panelQuickActions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelQuickActions.Name = "panelQuickActions";
            this.panelQuickActions.Size = new System.Drawing.Size(376, 546);
            this.panelQuickActions.TabIndex = 0;
            // 
            // btnQuick1
            // 
            this.btnQuick1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnQuick1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuick1.ForeColor = System.Drawing.Color.White;
            this.btnQuick1.Location = new System.Drawing.Point(4, 5);
            this.btnQuick1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQuick1.Name = "btnQuick1";
            this.btnQuick1.Size = new System.Drawing.Size(330, 62);
            this.btnQuick1.TabIndex = 0;
            this.btnQuick1.Text = "Nuevo Producto";
            this.btnQuick1.UseVisualStyleBackColor = false;
            this.btnQuick1.Click += new System.EventHandler(this.btnProductos_Click);
            // 
            // btnQuick2
            // 
            this.btnQuick2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnQuick2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuick2.ForeColor = System.Drawing.Color.White;
            this.btnQuick2.Location = new System.Drawing.Point(4, 77);
            this.btnQuick2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQuick2.Name = "btnQuick2";
            this.btnQuick2.Size = new System.Drawing.Size(330, 62);
            this.btnQuick2.TabIndex = 1;
            this.btnQuick2.Text = "Registrar Movimiento";
            this.btnQuick2.UseVisualStyleBackColor = false;
            this.btnQuick2.Click += new System.EventHandler(this.btnMovimientos_Click);
            // 
            // btnQuick3
            // 
            this.btnQuick3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.btnQuick3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuick3.ForeColor = System.Drawing.Color.White;
            this.btnQuick3.Location = new System.Drawing.Point(4, 149);
            this.btnQuick3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQuick3.Name = "btnQuick3";
            this.btnQuick3.Size = new System.Drawing.Size(330, 62);
            this.btnQuick3.TabIndex = 2;
            this.btnQuick3.Text = "Ver Lotes";
            this.btnQuick3.UseVisualStyleBackColor = false;
            this.btnQuick3.Click += new System.EventHandler(this.btnLotes_Click);
            // 
            // btnQuick4
            // 
            this.btnQuick4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnQuick4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuick4.ForeColor = System.Drawing.Color.White;
            this.btnQuick4.Location = new System.Drawing.Point(4, 221);
            this.btnQuick4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQuick4.Name = "btnQuick4";
            this.btnQuick4.Size = new System.Drawing.Size(330, 62);
            this.btnQuick4.TabIndex = 3;
            this.btnQuick4.Text = "Reportes";
            this.btnQuick4.UseVisualStyleBackColor = false;
            this.btnQuick4.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // btnTestDb
            // 
            this.btnTestDb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.btnTestDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestDb.ForeColor = System.Drawing.Color.White;
            this.btnTestDb.Location = new System.Drawing.Point(4, 293);
            this.btnTestDb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTestDb.Name = "btnTestDb";
            this.btnTestDb.Size = new System.Drawing.Size(330, 62);
            this.btnTestDb.TabIndex = 4;
            this.btnTestDb.Text = "Probar BD";
            this.btnTestDb.UseVisualStyleBackColor = false;
            this.btnTestDb.Click += new System.EventHandler(this.btnTestDb_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1650, 923);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelSidebar);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmPrincipal";
            this.Text = "Checkpoint - Principal";
            this.panelSidebar.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUser)).EndInit();
            this.panelContent.ResumeLayout(false);
            this.flowMetrics.ResumeLayout(false);
            this.panelTileAlerts.ResumeLayout(false);
            this.panelTileAlerts.PerformLayout();
            this.tableMain.ResumeLayout(false);
            this.groupChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxChart)).EndInit();
            this.groupRight.ResumeLayout(false);
            this.panelQuickActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Button btnLotes;
        private System.Windows.Forms.Button btnMovimientos;
        private System.Windows.Forms.Button btnSedesUbicaciones;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.PictureBox pictureBoxUser;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.FlowLayoutPanel flowMetrics;
        private System.Windows.Forms.Panel panelTileAlerts;
        private System.Windows.Forms.Label lblAlertsTitle;
        private System.Windows.Forms.Label lblAlertsValue;
        private System.Windows.Forms.TableLayoutPanel tableMain;
        private System.Windows.Forms.GroupBox groupChart;
        private System.Windows.Forms.PictureBox pictureBoxChart;
        private System.Windows.Forms.GroupBox groupRight;
        private System.Windows.Forms.FlowLayoutPanel panelQuickActions;
        private System.Windows.Forms.Button btnQuick1;
        private System.Windows.Forms.Button btnQuick2;
        private System.Windows.Forms.Button btnQuick3;
        private System.Windows.Forms.Button btnQuick4;
        private System.Windows.Forms.Button btnTestDb;
        private System.Windows.Forms.Button btnUsuarios; // AÑADIDO
        private System.Windows.Forms.CheckBox chkLightTheme; // THEME TOGGLE
    }
}