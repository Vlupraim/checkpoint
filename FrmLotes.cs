using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using Checkpoint.Data.Repositories; // Necesario
using Checkpoint.Core.Security;     // Necesario
using System.Linq;                  // 🎯 NECESARIO (Aunque cambiaremos el método)
using System.Collections.Generic;   // Necesario para List<T>

namespace checkpoint
{
    public partial class FrmLotes : Form
    {
        private readonly LoteRepository _repo = new LoteRepository();
        private bool _puedeGestionarCalidad = false;

        public FrmLotes()
        {
            InitializeComponent();
            this.Load += FrmLotes_Load;
        }

        private void FrmLotes_Load(object sender, EventArgs e)
        {
            AplicarSeguridad();
            LoadLotes();
        }

        private void AplicarSeguridad()
        {
            var roles = CurrentSession.Roles ?? new string[0];

            // 🎯 CORRECCIÓN (CS1929):
            // Convertimos los roles a minúscula para una comparación simple y segura.
            var rolesLower = roles.Select(r => r.ToLower()).ToList();

            _puedeGestionarCalidad = rolesLower.Contains("admin") ||
                                     rolesLower.Contains("control de calidad");

            btnNuevo.Enabled = _puedeGestionarCalidad;
            btnLiberar.Enabled = _puedeGestionarCalidad;
            btnBloquear.Enabled = _puedeGestionarCalidad;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Implementar creación de lote.", "Info");
        }

        private void btnLiberar_Click(object sender, EventArgs e)
        {
            ActualizarEstadoSeleccionado("Liberado");
        }

        private void btnBloquear_Click(object sender, EventArgs e)
        {
            ActualizarEstadoSeleccionado("Bloqueado");
        }

        private void ActualizarEstadoSeleccionado(string nuevoEstado)
        {
            if (dgvLotes.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un lote.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var loteId = (Guid)dgvLotes.CurrentRow.Cells["Id"].Value;
            var estadoActual = dgvLotes.CurrentRow.Cells["Estado"].Value?.ToString();

            if (estadoActual != null && estadoActual.Equals(nuevoEstado, StringComparison.OrdinalIgnoreCase))
            {
                // 🎯 CORRECCIÓN (CS0117): Era 'Info', debe ser 'Information'.
                MessageBox.Show($"El lote ya se encuentra en estado '{nuevoEstado}'.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string motivo = $"Cambio de estado a {nuevoEstado} por {CurrentSession.UsuarioActual?.Nombre ?? "Sistema"}";

            if (MessageBox.Show($"¿Confirma {nuevoEstado} el lote seleccionado?", "Confirmar Acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                _repo.ActualizarEstadoLote(loteId, nuevoEstado, motivo);
                MessageBox.Show($"Lote {nuevoEstado} exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLotes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el estado del lote: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            LoadLotes();
        }

        private void LoadLotes()
        {
            try
            {
                // Mantenemos el SQL directo para el JOIN con Producto
                var cs = ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
                if (string.IsNullOrEmpty(cs)) { MessageBox.Show("Cadena de conexión 'App' no encontrada."); return; }

                using (var conn = new SqlConnection(cs))
                using (var cmd = new SqlCommand("SELECT L.Id, L.CodigoLote, L.FechaIngreso, L.FechaVencimiento, P.Nombre AS Producto, L.Estado FROM Lote L LEFT JOIN Producto P ON L.ProductoId = P.Id ORDER BY L.FechaIngreso DESC", conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    dgvLotes.DataSource = dt;
                    if (dt.Columns.Contains("Id"))
                    {
                        dgvLotes.Columns["Id"].Visible = false; // Ocultar ID
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando lotes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}