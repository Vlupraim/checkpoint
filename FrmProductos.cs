using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Checkpoint.Data.Repositories;

namespace checkpoint
{
    public partial class FrmProductos : BaseForm
    {
        // Repositorio para operaciones de dominio (insert/update/delete/getById)
        private readonly ProductoRepository _repo = new ProductoRepository();

        public FrmProductos()
        {
            InitializeComponent();
            this.Load += FrmProductos_Load;

            // UX: doble-click para editar (si no lo conectaste en el Designer)
            dgvProductos.CellDoubleClick += dgvProductos_CellDoubleClick;
        }

        // ================== Eventos ==================
        private void FrmProductos_Load(object sender, EventArgs e)
        {
            LoadProductos();
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                EditarSeleccionado();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var f = new FrmDetalleProducto())
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                    LoadProductos();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarSeleccionado();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var idObj = dgvProductos.CurrentRow.Cells["Id"]?.Value;
            var nombre = dgvProductos.CurrentRow.Cells["Nombre"]?.Value?.ToString() ?? "(sin nombre)";

            if (idObj == null || !Guid.TryParse(idObj.ToString(), out Guid id))
            {
                MessageBox.Show("ID de producto inválido o no encontrado.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(
                $"¿Eliminar el producto \"{nombre}\"?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _repo.Delete(id);
                LoadProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo eliminar. Verifique dependencias (lotes/movimientos).\nDetalle: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            LoadProductos();
        }

        // ================== Lógica ==================
        private void EditarSeleccionado()
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto para editar.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var idObj = dgvProductos.CurrentRow.Cells["Id"]?.Value;
            if (idObj == null || !Guid.TryParse(idObj.ToString(), out Guid id))
            {
                MessageBox.Show("ID de producto inválido o no encontrado.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var f = new FrmDetalleProducto(id))
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                    LoadProductos();
            }
        }

        private void LoadProductos()
        {
            // Cadena desde App.config a través del shim en System.Configuration
            var cs = ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;

            if (string.IsNullOrWhiteSpace(cs))
            {
                MessageBox.Show("Cadena de conexión 'App' no encontrada en App.config.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var conn = new SqlConnection(cs))
                using (var cmd = new SqlCommand(
                    @"SELECT Id, Sku, Nombre, Unidad, VidaUtilDias, TempMin, TempMax, StockMinimo, Activo
                      FROM Producto
                      ORDER BY Nombre", conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    dgvProductos.DataSource = dt;
                    FormatearGrilla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando productos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatearGrilla()
        {
            if (dgvProductos.Columns.Count == 0) return;

            if (dgvProductos.Columns.Contains("Id"))
                dgvProductos.Columns["Id"].Visible = false;

            SetHeader("Sku", "SKU", 90);
            SetHeader("Nombre", "Nombre", 200);
            SetHeader("Unidad", "Unidad", 90);
            SetHeader("VidaUtilDias", "Vida útil (días)", 110);
            SetHeader("TempMin", "Temp. Mín.", 100);
            SetHeader("TempMax", "Temp. Máx.", 100);
            SetHeader("StockMinimo", "Stock mínimo", 110);
            SetHeader("Activo", "Activo", 70);

            dgvProductos.AutoResizeColumns();
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
        }

        private void SetHeader(string columnName, string headerText, int width)
        {
            if (!dgvProductos.Columns.Contains(columnName)) return;
            var c = dgvProductos.Columns[columnName];
            c.HeaderText = headerText;
            c.Width = width;
        }
    }
}
