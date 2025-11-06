using System;
using System.Windows.Forms;
using Checkpoint.Data.Repositories;
using Checkpoint.Core.Security;

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
                dgvUsuarios.DataSource = _repo.GetAll();
                if (dgvUsuarios.Columns["Id"] != null)
                    dgvUsuarios.Columns["Id"].Visible = false; // oculto id en grilla

                dgvUsuarios.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando usuarios: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Guid? GetSelectedUserId()
        {
            if (dgvUsuarios.CurrentRow == null) return null;
            var cell = dgvUsuarios.CurrentRow.Cells["Id"];
            if (cell == null || cell.Value == null) return null;
            return (Guid)cell.Value;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var f = new FrmDetalleUsuario())
            {
                if (f.ShowDialog() == DialogResult.OK)
                    LoadUsuarios();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var id = GetSelectedUserId();
            if (id == null)
            {
                MessageBox.Show("Seleccione un usuario para editar.", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var f = new FrmDetalleUsuario(id.Value))
            {
                if (f.ShowDialog() == DialogResult.OK)
                    LoadUsuarios();
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            LoadUsuarios();
        }

        // 🔴 Eliminar DEFINITIVO
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var id = GetSelectedUserId();
            if (id == null)
            {
                MessageBox.Show("Seleccione un usuario.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Evitar eliminarme a mí mismo
            if (CurrentSession.UsuarioActual != null && CurrentSession.UsuarioActual.Id == id.Value)
            {
                MessageBox.Show("No puedes eliminar el usuario con el que estás logueado.",
                    "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dr = MessageBox.Show(
                "¿Eliminar definitivamente al usuario seleccionado?\n(Esta acción no se puede deshacer)",
                "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes) return;

            try
            {
                _repo.Delete(id.Value);
                LoadUsuarios();
                MessageBox.Show("Usuario eliminado.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo eliminar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🟡 Desactivar (soft delete)
        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            var id = GetSelectedUserId();
            if (id == null)
            {
                MessageBox.Show("Seleccione un usuario.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var fila = dgvUsuarios.CurrentRow;
            bool activo = false;

            // intenta leer el valor de la columna Activo (puede ser bool o string)
            try
            {
                activo = Convert.ToBoolean(fila.Cells["Activo"].Value);
            }
            catch
            {
                bool.TryParse(fila.Cells["Activo"].Value?.ToString(), out activo);
            }

            if (CurrentSession.UsuarioActual != null && CurrentSession.UsuarioActual.Id == id.Value)
            {
                MessageBox.Show("No puedes cambiar tu propio estado mientras estás en sesión.",
                    "Operación no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool nuevoEstado = !activo;
            string accion = nuevoEstado ? "activar" : "desactivar";

            var dr = MessageBox.Show(
                $"¿Desea {accion} al usuario seleccionado?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes) return;

            try
            {
                _repo.SetActivo(id.Value, nuevoEstado);
                LoadUsuarios();
                MessageBox.Show($"Usuario {(nuevoEstado ? "activado" : "desactivado")} correctamente.",
                    "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

}
