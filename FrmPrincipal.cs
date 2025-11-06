using System;
using System.Windows.Forms;
using Checkpoint.Data;

namespace checkpoint
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            this.Load += FrmPrincipal_Load;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            // Inicialización de la pantalla principal
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            using (var f = new FrmProductos())
            {
                f.ShowDialog(this);
            }
        }

        private void btnLotes_Click(object sender, EventArgs e)
        {
            using (var f = new FrmLotes())
            {
                f.ShowDialog(this);
            }
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            using (var f = new FrmMovimientos())
            {
                f.ShowDialog(this);
            }
        }

        private void btnSedesUbicaciones_Click(object sender, EventArgs e)
        {
            using (var f = new FrmSedesUbicaciones())
            {
                f.ShowDialog(this);
            }
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            using (var f = new FrmReportes())
            {
                f.ShowDialog(this);
            }
        }

        // NUEVO: Handler para el botón de Gestión de Usuarios
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            using (var f = new FrmGestionUsuarios())
            {
                f.ShowDialog(this);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Cerrar sesión simple
            Application.Exit();
        }

        private void btnTestDb_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseInitializer.EnsureDatabase();
                MessageBox.Show("Inicialización DB ejecutada. Revisa output/log para detalles.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inicializando base: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblAlertsTitle_Click(object sender, EventArgs e)
        {

        }
    }
}