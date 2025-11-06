using System;
using System.Windows.Forms;

namespace checkpoint
{
    // Asegúrate de que este formulario exista en tu proyecto.
    public partial class FrmBodegaDashboard : Form
    {
        public FrmBodegaDashboard()
        {
            InitializeComponent();
            this.Text = "CheckPoint - Panel de Personal de Bodega";
        }

        // Asume que tienes un botón llamado btnMovimientos en el Designer
        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            // Ir directamente a la pantalla de trabajo
            using (var f = new FrmMovimientos())
            {
                f.ShowDialog(this);
            }
        }
    }
}