using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace checkpoint
{
 public partial class FrmReportes : BaseForm
 {
 public FrmReportes()
 {
 InitializeComponent();
 this.Load += FrmReportes_Load;
 }

 private void FrmReportes_Load(object sender, EventArgs e)
 {
 // Cargar filtros por defecto
 }

 private void btnStockPorUbicacion_Click(object sender, EventArgs e)
 {
 var cs = ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
 if (string.IsNullOrEmpty(cs)) { MessageBox.Show("Cadena 'App' no encontrada."); return; }
 try
 {
 using (var conn = new SqlConnection(cs))
 using (var cmd = new SqlCommand("SELECT U.Codigo AS Ubicacion, SUM(S.Cantidad) AS Cantidad FROM Stock S JOIN Ubicacion U ON S.UbicacionId = U.Id GROUP BY U.Codigo", conn))
 using (var da = new SqlDataAdapter(cmd))
 {
 var dt = new DataTable();
 da.Fill(dt);
 dgvReport.DataSource = dt;
 }
 }
 catch (Exception ex)
 {
 MessageBox.Show("Error generando reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 }
 }
 }
}
