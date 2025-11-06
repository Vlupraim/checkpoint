using System;
using System.Windows.Forms;

namespace checkpoint
{
    // Asegúrate de que este formulario exista en tu proyecto.
    public partial class FrmCalidadDashboard : Form
    {
        public FrmCalidadDashboard()
        {
            InitializeComponent();
            this.Text = "CheckPoint - Panel de Control de Calidad";
        }

        // Asume que tienes un botón llamado btnLotes en el Designer
        private void btnLotes_Click(object sender, EventArgs e)
        {
            // Ir directamente a la pantalla de trabajo
            using (var f = new FrmLotes())
            {
                f.ShowDialog(this);
            }
        }
    }
}