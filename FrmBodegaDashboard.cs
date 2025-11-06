using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Checkpoint.Data.Repositories;

namespace checkpoint
{
    public partial class FrmBodegaDashboard : Form
    {
        // Repos
        private readonly ProductoRepository _prodRepo = new ProductoRepository();
        private readonly LoteRepository _loteRepo = new LoteRepository();
        private readonly MovimientoRepository _movRepo = new MovimientoRepository();

        // ====== Theme ======
        readonly Color BgForm = Color.FromArgb(245, 247, 250);   // fondo gris claro
        readonly Color CardBg = Color.FromArgb(255, 255, 255);   // blanco
        readonly Color CardBorder = Color.FromArgb(223, 228, 235);   // borde suave
        readonly Color Accent = Color.FromArgb(51, 102, 255);    // azul
        readonly Color TextMain = Color.FromArgb(33, 37, 41);      // casi negro
        readonly Color TextSoft = Color.FromArgb(110, 120, 130);   // gris texto secundario

        public FrmBodegaDashboard()
        {
            InitializeComponent();
        }

        // Anti-flicker
        protected override CreateParams CreateParams
        {
            get { var p = base.CreateParams; p.ExStyle |= 0x02000000; return p; } // WS_EX_COMPOSITED
        }

        private void FrmBodegaDashboard_Load(object sender, EventArgs e)
        {
            ApplyTheme();
            ResizeTiles();
            CargarKpisYAlertas();

            lblStockBajoTxt.Text = "Productos bajo stock mínimo";
            lblPorVencerTxt.Text = "Lotes por vencer (30 días)";
            lblPendientesTxt.Text = "Lotes pendientes recepción";

            flKpis.Resize += (s, ev) => ResizeTiles();

            // Doble click en alertas para abrir forms relacionados
            dgvAlertas.CellDoubleClick += (s, ev) =>
            {
                if (ev.RowIndex < 0) return;
                var tipo = dgvAlertas.Rows[ev.RowIndex].Cells[0].Value?.ToString();
                if (tipo == "Vencimiento")
                {
                    using (var f = new FrmLotes())
                    {
                        f.ShowDialog(this);
                    }
                }
                else if (tipo == "Stock")
                {
                    using (var f = new FrmProductos())
                    {
                        f.ShowDialog(this);
                    }
                }
            };
        }

        // ====== Estilos ======
        void ApplyTheme()
        {
            // Form
            BackColor = BgForm;
            ForeColor = TextMain;
            MinimumSize = new Size(960, 560);

            // Tiles
            StyleTile(tileStockBajo, lblStockBajoNum, lblStockBajoTxt);
            StyleTile(tilePorVencer, lblPorVencerNum, lblPorVencerTxt);
            StyleTile(tilePendientes, lblPendientesNum, lblPendientesTxt);

            // Flow KPIs
            flKpis.BackColor = BgForm;
            flKpis.Padding = new Padding(6);
            flKpis.FlowDirection = FlowDirection.LeftToRight;
            flKpis.WrapContents = true;

            // Botones acciones
            foreach (var b in new[] { btnMovimientoInterno, btnSalida, btnDevolucionAjuste, btnInventarioCiclico, btnRefrescar })
            {
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderColor = CardBorder;
                b.FlatAppearance.BorderSize = 1;
                b.BackColor = CardBg;
                b.ForeColor = TextMain;
                b.Height = 46;
            }

            // GroupBoxes
            gbAcciones.BackColor = BgForm;
            gbAcciones.ForeColor = TextSoft;
            gbAlertas.BackColor = BgForm;
            gbAlertas.ForeColor = TextSoft;

            // Grilla
            dgvAlertas.EnableHeadersVisualStyles = false;
            dgvAlertas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAlertas.BackgroundColor = CardBg;
            dgvAlertas.BorderStyle = BorderStyle.None;
            dgvAlertas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 242, 246);
            dgvAlertas.ColumnHeadersDefaultCellStyle.ForeColor = TextMain;
            dgvAlertas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvAlertas.DefaultCellStyle.BackColor = CardBg;
            dgvAlertas.DefaultCellStyle.ForeColor = TextMain;
            dgvAlertas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 238, 255);
            dgvAlertas.DefaultCellStyle.SelectionForeColor = TextMain;
            dgvAlertas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 252);
            dgvAlertas.RowHeadersVisible = false;
            dgvAlertas.GridColor = CardBorder;
        }

        void StyleTile(Panel p, Label num, Label txt)
        {
            p.BackColor = CardBg;
            p.BorderStyle = BorderStyle.None;
            p.Padding = new Padding(14);
            p.Paint -= PaintRoundedPanel; // evitar múltiple suscripción
            p.Paint += PaintRoundedPanel;

            num.ForeColor = Accent;
            num.Font = new Font("Segoe UI", 26, FontStyle.Bold);
            num.AutoSize = true;

            txt.ForeColor = TextSoft;
            txt.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            txt.AutoSize = true;
            txt.Top = num.Bottom + 6;
            txt.Left = num.Left;
        }

        void PaintRoundedPanel(object sender, PaintEventArgs e)
        {
            var panel = (Panel)sender;
            var rect = panel.ClientRectangle; rect.Inflate(-1, -1);

            using (var path = new GraphicsPath())
            {
                int r = 14;
                path.AddArc(rect.X, rect.Y, r, r, 180, 90);
                path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
                path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
                path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);
                path.CloseFigure();

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var br = new SolidBrush(CardBg))
                using (var pen = new Pen(CardBorder, 1))
                {
                    e.Graphics.FillPath(br, path);
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        void ResizeTiles()
        {
            int maxWidth = flKpis.ClientSize.Width;
            int tileWidth = Math.Min(300, Math.Max(220, (maxWidth - 40) / 3)); // 3 por fila si cabe
            foreach (Control c in flKpis.Controls)
                if (c is Panel) ((Panel)c).Width = tileWidth;
        }

        // ====== Datos ======
        private void CargarKpisYAlertas()
        {
            try
            {
                // KPIs
                lblStockBajoNum.Text = _prodRepo.GetProductosBajoStockMinimoCount().ToString();
                lblPorVencerNum.Text = _loteRepo.GetLotesPorVencerCount(30).ToString();
                lblPendientesNum.Text = _loteRepo.GetPendientesRecepcionCount().ToString();

                // Colorear por severidad
                ColorKpi(lblStockBajoNum.Text, tileStockBajo);
                ColorKpi(lblPorVencerNum.Text, tilePorVencer);
                ColorKpi(lblPendientesNum.Text, tilePendientes);

                // Alertas combinadas
                var porVencer = _loteRepo.GetLotesPorVencer(30, top: 10);
                var bajoStock = _prodRepo.GetProductosBajoStockMinimo(top: 10);

                var dt = new DataTable();
                dt.Columns.Add("Tipo");
                dt.Columns.Add("Codigo/Producto");
                dt.Columns.Add("Detalle");
                dt.Columns.Add("Fecha/Stock");

                foreach (var l in porVencer)
                    dt.Rows.Add("Vencimiento", l.CodigoLote, "ProdId: " + l.ProductoId, l.FechaVencimiento?.ToString("yyyy-MM-dd"));

                foreach (var p in bajoStock)
                    dt.Rows.Add("Stock", p.Sku, p.Nombre, "Bajo mínimo");

                dgvAlertas.DataSource = dt;
                dgvAlertas.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar KPIs/alertas: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ColorKpi(string valorTexto, Panel tile)
        {
            int val;
            if (!int.TryParse(valorTexto, out val)) val = 0;
            // rojo >10, naranjo 1–10, verde 0
            var col = val == 0 ? Color.FromArgb(230, 245, 233) :
                      val <= 10 ? Color.FromArgb(255, 245, 230) :
                                  Color.FromArgb(255, 235, 238);
            tile.BackColor = col;
            tile.Invalidate();
        }

        // ====== Acciones (abre formularios existentes) ======
        private void btnMovimientoInterno_Click(object sender, EventArgs e)
        {
            using (var f = new FrmMovimientos())
            {
                f.ShowDialog(this);
            }
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            using (var f = new FrmMovimientos())
            {
                f.ShowDialog(this);
            }
        }

        private void btnDevolucionAjuste_Click(object sender, EventArgs e)
        {
            using (var f = new FrmMovimientos())
            {
                f.ShowDialog(this);
            }
        }

        private void btnInventarioCiclico_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Inventario cíclico: pendiente de implementar.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarKpisYAlertas();
        }
    }
}
