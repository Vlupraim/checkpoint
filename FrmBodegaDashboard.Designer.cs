using System.Windows.Forms;

namespace checkpoint
{
    partial class FrmBodegaDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private TableLayoutPanel tlMain;
        private GroupBox gbAcciones;
        private FlowLayoutPanel flKpis;
        private Button btnMovimientoInterno;
        private Button btnSalida;
        private Button btnDevolucionAjuste;
        private Button btnInventarioCiclico;
        private Button btnRefrescar;
        private GroupBox gbAlertas;
        private DataGridView dgvAlertas;
        private Panel tileStockBajo;
        private Label lblStockBajoNum;
        private Label lblStockBajoTxt;
        private Panel tilePorVencer;
        private Label lblPorVencerNum;
        private Label lblPorVencerTxt;
        private Panel tilePendientes;
        private Label lblPendientesNum;
        private Label lblPendientesTxt;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tlMain = new TableLayoutPanel();
            this.gbAcciones = new GroupBox();
            this.btnMovimientoInterno = new Button();
            this.btnSalida = new Button();
            this.btnDevolucionAjuste = new Button();
            this.btnInventarioCiclico = new Button();
            this.btnRefrescar = new Button();
            this.flKpis = new FlowLayoutPanel();
            this.tileStockBajo = new Panel();
            this.lblStockBajoNum = new Label();
            this.lblStockBajoTxt = new Label();
            this.tilePorVencer = new Panel();
            this.lblPorVencerNum = new Label();
            this.lblPorVencerTxt = new Label();
            this.tilePendientes = new Panel();
            this.lblPendientesNum = new Label();
            this.lblPendientesTxt = new Label();
            this.gbAlertas = new GroupBox();
            this.dgvAlertas = new DataGridView();
            // 
            // tlMain
            // 
            this.tlMain.ColumnCount = 2;
            this.tlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            this.tlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            this.tlMain.Dock = DockStyle.Fill;
            this.tlMain.RowCount = 2;
            this.tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            this.tlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            // 
            // gbAcciones
            // 
            this.gbAcciones.Text = "Acciones rápidas";
            this.gbAcciones.Dock = DockStyle.Fill;
            this.gbAcciones.Padding = new Padding(10);
            this.gbAcciones.Controls.Add(this.btnMovimientoInterno);
            this.gbAcciones.Controls.Add(this.btnSalida);
            this.gbAcciones.Controls.Add(this.btnDevolucionAjuste);
            this.gbAcciones.Controls.Add(this.btnInventarioCiclico);
            this.gbAcciones.Controls.Add(this.btnRefrescar);
            // 
            // botones
            // 
            int w = 220, h = 42, sp = 10, x = 15, y = 25;
            void place(Button b, string text)
            {
                b.Text = text; b.Width = w; b.Height = h; b.Left = x; b.Top = y; b.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                y += h + sp; this.gbAcciones.Controls.Add(b);
            }
            place(this.btnMovimientoInterno, "Movimientos internos");
            place(this.btnSalida, "Salidas a Producción/Venta");
            place(this.btnDevolucionAjuste, "Devoluciones / Ajustes");
            place(this.btnInventarioCiclico, "Inventario Cíclico");
            place(this.btnRefrescar, "Refrescar tablero");
            this.btnMovimientoInterno.Click += btnMovimientoInterno_Click;
            this.btnSalida.Click += btnSalida_Click;
            this.btnDevolucionAjuste.Click += btnDevolucionAjuste_Click;
            this.btnInventarioCiclico.Click += btnInventarioCiclico_Click;
            this.btnRefrescar.Click += btnRefrescar_Click;
            // 
            // flKpis
            // 
            this.flKpis.Dock = DockStyle.Fill;
            this.flKpis.WrapContents = true;
            this.flKpis.AutoScroll = true;
            // 
            // Tiles estilo simple
            // 
            Panel MakeTile(Panel p, Label num, Label txt, string caption)
            {
                p.Width = 220; p.Height = 100; p.Margin = new Padding(10); p.BorderStyle = BorderStyle.FixedSingle;
                num.Text = "0"; num.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
                num.AutoSize = true; num.Top = 15; num.Left = 15;
                txt.Text = caption; txt.Font = new System.Drawing.Font("Segoe UI", 10F); txt.AutoSize = true; txt.Top = 65; txt.Left = 15;
                p.Controls.Add(num); p.Controls.Add(txt);
                return p;
            }
            this.flKpis.Controls.Add(MakeTile(this.tileStockBajo, this.lblStockBajoNum, this.lblStockBajoTxt, "Productos bajo stock mínimo"));
            this.flKpis.Controls.Add(MakeTile(this.tilePorVencer, this.lblPorVencerNum, this.lblPorVencerTxt, "Lotes por vencer (30 días)"));
            this.flKpis.Controls.Add(MakeTile(this.tilePendientes, this.lblPendientesNum, this.lblPendientesTxt, "Lotes pendientes recepción"));
            // 
            // gbAlertas
            // 
            this.gbAlertas.Text = "Alertas";
            this.gbAlertas.Dock = DockStyle.Fill;
            this.gbAlertas.Padding = new Padding(8);
            // 
            // dgvAlertas
            // 
            this.dgvAlertas.Dock = DockStyle.Fill;
            this.dgvAlertas.ReadOnly = true;
            this.dgvAlertas.AllowUserToAddRows = false;
            this.dgvAlertas.AllowUserToDeleteRows = false;
            this.dgvAlertas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gbAlertas.Controls.Add(this.dgvAlertas);

            // add to layout
            this.tlMain.Controls.Add(this.gbAcciones, 0, 0);
            this.tlMain.Controls.Add(this.flKpis, 1, 0);
            this.tlMain.SetColumnSpan(this.gbAlertas, 2);
            this.tlMain.Controls.Add(this.gbAlertas, 0, 1);

            // Frm
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Text = "Bodega — Tablero";
            this.ClientSize = new System.Drawing.Size(980, 600);
            this.Controls.Add(this.tlMain);
            this.Load += FrmBodegaDashboard_Load;
        }
    }
}
