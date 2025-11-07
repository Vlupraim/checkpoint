using System;
using System.Windows.Forms;

namespace checkpoint
{
 public partial class FrmMovimientos : BaseForm
 {
 public FrmMovimientos()
 {
 InitializeComponent();
 this.Load += FrmMovimientos_Load;
 }

 private void FrmMovimientos_Load(object sender, EventArgs e)
 {
 // TODO: cargar comboboxes de sedes, ubicaciones, lotes, productos
 }

 private void btnRegistrarIngreso_Click(object sender, EventArgs e)
 {
 MessageBox.Show("Implementar RegistrarIngreso -> llamar a MovimientoRepository.", "Info");
 }

 private void btnRegistrarSalida_Click(object sender, EventArgs e)
 {
 MessageBox.Show("Implementar RegistrarSalida -> llamar a MovimientoRepository.", "Info");
 }

 private void btnRegistrarMovimiento_Click(object sender, EventArgs e)
 {
 MessageBox.Show("Implementar RegistrarMovimientoInterno -> llamar a MovimientoRepository.", "Info");
 }

 private void btnRegistrarDevolucion_Click(object sender, EventArgs e)
 {
 MessageBox.Show("Implementar RegistrarDevolucion -> llamar a MovimientoRepository.", "Info");
 }

 private void btnRegistrarAjuste_Click(object sender, EventArgs e)
 {
 MessageBox.Show("Implementar RegistrarAjuste -> llamar a MovimientoRepository.", "Info");
 }
 }
}
