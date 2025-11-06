using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Checkpoint.Core.Entities;
using Checkpoint.Core.Security; // Necesario para PasswordHasher
using Checkpoint.Data.Repositories;

namespace checkpoint
{
    public partial class FrmDetalleUsuario : Form
    {
        private readonly UsuarioRepository _userRepo = new UsuarioRepository();
        private readonly RolRepository _rolRepo = new RolRepository();

        private Guid? _usuarioId = null;
        private string _passwordHashActual = null;
        private List<Rol> _listaRolesSistema;

        public FrmDetalleUsuario()
        {
            InitializeComponent();
            this.Text = "Nuevo Usuario";
        }

        public FrmDetalleUsuario(Guid id) : this()
        {
            _usuarioId = id;
            this.Text = "Editar Usuario";
            lblPasswordInfo.Visible = true; // Mostrar ayuda de contraseña
        }

        private void FrmDetalleUsuario_Load(object sender, EventArgs e)
        {
            CargarRolesDisponibles();

            if (_usuarioId.HasValue)
            {
                CargarDatosUsuario(_usuarioId.Value);
            }
        }

        private void CargarRolesDisponibles()
        {
            _listaRolesSistema = _rolRepo.GetAllEntities().ToList();

            // Configurar el CheckedListBox
            chkRoles.DataSource = _listaRolesSistema;
            chkRoles.DisplayMember = "Nombre";
            chkRoles.ValueMember = "Id";
        }

        private void CargarDatosUsuario(Guid id)
        {
            try
            {
                var usuario = _userRepo.GetById(id);
                if (usuario == null)
                {
                    MessageBox.Show("Usuario no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                txtNombre.Text = usuario.Nombre;
                txtEmail.Text = usuario.Email;
                chkActivo.Checked = usuario.Activo;
                _passwordHashActual = usuario.PasswordHash; // Guardamos el hash actual

                // Marcar los roles que tiene el usuario
                var rolesUsuario = _userRepo.GetRoleIds(id).ToList();
                for (int i = 0; i < chkRoles.Items.Count; i++)
                {
                    var rol = (Rol)chkRoles.Items[i];
                    if (rolesUsuario.Contains(rol.Id))
                    {
                        chkRoles.SetItemChecked(i, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando datos del usuario: " + ex.Message, "Error");
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            try
            {
                var usuario = new Usuario
                {
                    Nombre = txtNombre.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Activo = chkActivo.Checked
                };

                // Obtener los roles seleccionados
                var rolesSeleccionados = new List<Guid>();
                foreach (var item in chkRoles.CheckedItems)
                {
                    rolesSeleccionados.Add(((Rol)item).Id);
                }

                if (_usuarioId.HasValue) // Modo Edición
                {
                    usuario.Id = _usuarioId.Value;

                    // Solo actualizar contraseña si se escribió una nueva
                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        usuario.PasswordHash = PasswordHasher.CreateHash(txtPassword.Text);
                    }
                    else
                    {
                        usuario.PasswordHash = _passwordHashActual; // Mantener el hash anterior
                    }

                    _userRepo.Update(usuario, rolesSeleccionados);
                }
                else // Modo Creación
                {
                    usuario.Id = Guid.NewGuid();
                    usuario.PasswordHash = PasswordHasher.CreateHash(txtPassword.Text);

                    _userRepo.Insert(usuario, rolesSeleccionados);
                }

                MessageBox.Show("Usuario guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El Nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("El Email es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!_usuarioId.HasValue && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("La Contraseña es obligatoria para nuevos usuarios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (chkRoles.CheckedItems.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos un rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}