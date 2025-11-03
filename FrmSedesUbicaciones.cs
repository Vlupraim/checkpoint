using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace checkpoint
{
 public partial class FrmSedesUbicaciones : Form
 {
 public FrmSedesUbicaciones()
 {
 InitializeComponent();
 this.Load += FrmSedesUbicaciones_Load;
 }

 private void FrmSedesUbicaciones_Load(object sender, EventArgs e)
 {
 LoadSedes();
 LoadUbicaciones();
 }

 private void btnNuevaSede_Click(object sender, EventArgs e)
 {
 MessageBox.Show("Implementar CRUD de Sede.", "Info");
 }

 private void btnNuevaUbicacion_Click(object sender, EventArgs e)
 {
 MessageBox.Show("Implementar CRUD de Ubicación.", "Info");
 }

 private void btnRefrescar_Click(object sender, EventArgs e)
 {
 LoadSedes();
 LoadUbicaciones();
 }

 private void LoadSedes()
 {
 var cs = ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
 if (string.IsNullOrEmpty(cs)) { MessageBox.Show("Cadena 'App' no encontrada."); return; }
 try
 {
 using (var conn = new SqlConnection(cs))
 using (var cmd = new SqlCommand("SELECT Id, Nombre, Codigo, Direccion, Activa FROM Sede ORDER BY Nombre", conn))
 using (var da = new SqlDataAdapter(cmd))
 {
 var dt = new DataTable();
 da.Fill(dt);
 dgvSedes.DataSource = dt;
 }
 }
 catch (Exception ex)
 {
 MessageBox.Show("Error cargando sedes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 }
 }

 private void LoadUbicaciones()
 {
 var cs = ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
 if (string.IsNullOrEmpty(cs)) return;
 try
 {
 using (var conn = new SqlConnection(cs))
 using (var cmd = new SqlCommand("SELECT U.Id, U.Codigo, U.Tipo, U.Capacidad, S.Nombre AS Sede FROM Ubicacion U JOIN Sede S ON U.SedeId = S.Id ORDER BY S.Nombre, U.Codigo", conn))
 using (var da = new SqlDataAdapter(cmd))
 {
 var dt = new DataTable();
 da.Fill(dt);
 dgvUbicaciones.DataSource = dt;
 }
 }
 catch (Exception ex)
 {
 MessageBox.Show("Error cargando ubicaciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
 }
 }
 }
}
