using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace checkpoint
{
 public partial class FrmLotes : Form
 {
 public FrmLotes()
 {
 InitializeComponent();
 this.Load += FrmLotes_Load;
 }

 private void FrmLotes_Load(object sender, EventArgs e)
 {
 LoadLotes();
 }

 private void btnNuevo_Click(object sender, EventArgs e)
 {
 MessageBox.Show("Implementar creación de lote.", "Info");
 }

 private void btnLiberar_Click(object sender, EventArgs e)
 {
 MessageBox.Show("Implementar liberación de lote.", "Info");
 }

 private void btnBloquear_Click(object sender, EventArgs e)
 {
 MessageBox.Show("Implementar bloqueo de lote.", "Info");
 }

 private void btnRefrescar_Click(object sender, EventArgs e)
 {
 LoadLotes();
 }

 private void LoadLotes()
 {
 var cs = ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
 if (string.IsNullOrEmpty(cs)) { MessageBox.Show("Cadena de conexión 'App' no encontrada."); return; }

 try
 {
 using (var conn = new SqlConnection(cs))
 using (var cmd = new SqlCommand("SELECT L.Id, L.CodigoLote, L.FechaIngreso, L.FechaVencimiento, P.Nombre AS Producto, L.Estado FROM Lote L LEFT JOIN Producto P ON L.ProductoId = P.Id ORDER BY L.FechaIngreso DESC", conn))
 using (var da = new SqlDataAdapter(cmd))
 {
 var dt = new DataTable();
 da.Fill(dt);
 dgvLotes.DataSource = dt;
 }
 }
 catch (Exception ex)
 {
 MessageBox.Show("Error cargando lotes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 }
 }
 }
}
