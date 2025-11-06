using System;
using System.Windows.Forms;
using Checkpoint.Data.Repositories;

namespace checkpoint
{
    public partial class FrmGestionUsuarios : Form
    {
        private readonly UsuarioRepository _repo = new UsuarioRepository();

        public FrmGestionUsuarios()
        {
            InitializeComponent();
        }

        private void FrmGestionUsuarios_Load(object sender, EventArgs e)
        {
            LoadUsuarios();
        }

        private void LoadUsuarios()
        {
            try
            {
                // Usamos el método GetAll() que actualizamos en el repositorio
                dgvUsuarios.DataSource = _repo.GetAll();
                dgvUsuarios.Columns["Id"].Visible = false; // Ocultar ID
                dgvUsuarios.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // Abrir el formulario de detalle en modo Creación
            using (var f = new FrmDetalleUsuario())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadUsuarios(); // Recargar la lista si se guardó
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un usuario para editar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var id = (Guid)dgvUsuarios.CurrentRow.Cells["Id"].Value;

            // Abrir el formulario de detalle en modo Edición
            using (var f = new FrmDetalleUsuario(id))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadUsuarios(); // Recargar la lista si se guardó
                }
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            LoadUsuarios();
        }
    }
}