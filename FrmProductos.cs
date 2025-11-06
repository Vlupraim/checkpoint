using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using Checkpoint.Data.Repositories; // Necesario para usar ProductoRepository

namespace checkpoint
{
    public partial class FrmProductos : Form
    {
        // Instancia del repositorio para la capa de datos
        private readonly ProductoRepository _repo = new ProductoRepository();

        public FrmProductos()
        {
            InitializeComponent();
            this.Load += FrmProductos_Load;
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            LoadProductos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // Lógica para abrir en modo CREACIÓN
            using (var f = new FrmDetalleProducto())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadProductos(); // Recarga la lista si se guardó correctamente
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto para editar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Obtener el ID del producto seleccionado
            var idProductoObj = dgvProductos.CurrentRow.Cells["Id"].Value;
            if (idProductoObj == null || !Guid.TryParse(idProductoObj.ToString(), out Guid idProducto))
            {
                MessageBox.Show("ID de producto inválido o no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Abrir FrmDetalleProducto en modo EDICIÓN
            using (var f = new FrmDetalleProducto(idProducto))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadProductos(); // Recarga la lista si la edición fue exitosa
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var id = dgvProductos.CurrentRow.Cells["Id"].Value;
            var nombre = dgvProductos.CurrentRow.Cells["Nombre"].Value;

            if (MessageBox.Show($"¿Eliminar producto '{nombre}' seleccionado?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeleteProducto(id);
                LoadProductos();
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            LoadProductos();
        }

        private void LoadProductos()
        {
            // La lógica de carga queda con ADO.NET SQL Command directo (como estaba originalmente)
            var cs = ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
            if (string.IsNullOrEmpty(cs))
            {
                MessageBox.Show("Cadena de conexión 'App' no encontrada en app.config.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var conn = new SqlConnection(cs))
                using (var cmd = new SqlCommand("SELECT Id, Sku, Nombre, Unidad, VidaUtilDias, TempMin, TempMax, StockMinimo, Activo FROM Producto ORDER BY Nombre", conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    dgvProductos.DataSource = dt;
                    dgvProductos.AutoResizeColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteProducto(object id)
        {
            if (id == null) return;
            try
            {
                // **USO DEL REPOSITORIO:** Llamamos al método Delete del ProductoRepository
                _repo.Delete((Guid)id);
            }
            catch (Exception ex)
            {
                // Captura el error si el producto tiene relaciones (ej. con Lotes)
                MessageBox.Show("Error eliminando producto. Asegúrate de que no tenga dependencias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}