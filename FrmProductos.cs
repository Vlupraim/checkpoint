using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace checkpoint
{
 public partial class FrmProductos : Form
 {
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
 MessageBox.Show("Implementar formulario detalle de producto.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
 }

 private void btnEditar_Click(object sender, EventArgs e)
 {
 if (dgvProductos.CurrentRow == null) { MessageBox.Show("Seleccione un producto.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
 var id = dgvProductos.CurrentRow.Cells["Id"].Value;
 MessageBox.Show("Editar producto Id=" + id?.ToString(), "Info");
 }

 private void btnEliminar_Click(object sender, EventArgs e)
 {
 if (dgvProductos.CurrentRow == null) { MessageBox.Show("Seleccione un producto.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
 var id = dgvProductos.CurrentRow.Cells["Id"].Value;
 if (MessageBox.Show("Eliminar producto seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
 var cs = ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
 try
 {
 using (var conn = new SqlConnection(cs))
 using (var cmd = new SqlCommand("DELETE FROM Producto WHERE Id = @id", conn))
 {
 cmd.Parameters.AddWithValue("@id", id);
 conn.Open();
 cmd.ExecuteNonQuery();
 }
 }
 catch (Exception ex)
 {
 MessageBox.Show("Error eliminando producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 }
 }
 }
}
