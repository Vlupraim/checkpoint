using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using Checkpoint.Core.Security;
using System.Collections.Generic; // Necesario para List<T>
using Checkpoint.Data.Repositories;

namespace checkpoint
{
    public partial class FrmLogin : Form
    {
        private readonly AuthenticationService _auth = new AuthenticationService();

        public FrmLogin()
        {
            InitializeComponent();
            this.Load += FrmLogin_Load;
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            CenterLoginBox();
            this.SizeChanged += (s, ev) => CenterLoginBox();
        }

        private void CenterLoginBox()
        {
            if (panelLoginBox != null)
            {
                int x = (this.ClientSize.Width - panelLoginBox.Width) / 2;
                int y = (this.ClientSize.Height - panelLoginBox.Height) / 2;
                panelLoginBox.Location = new System.Drawing.Point(x, y);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text.Trim(); // Corrección de Trim

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Email y contraseña son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var res = _auth.Authenticate(email, password);

            if (!res.Success)
            {
                MessageBox.Show(res.Message, "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Establecer la sesión
            CurrentSession.UsuarioActual = res.User;
            CurrentSession.Roles = res.Roles;

            this.Hide();

            // Lógica de Redirección
            Form formToOpen = null;

            // 🎯 CORRECCIÓN (CS1929): Convertir a minúsculas para comparación
            var rolesLower = CurrentSession.Roles.Select(r => r.ToLower()).ToList();

            if (rolesLower.Contains("personal de bodega"))
            {
                formToOpen = new FrmBodegaDashboard();
            }
            else if (rolesLower.Contains("control de calidad"))
            {
                formToOpen = new FrmCalidadDashboard();
            }
            else
            {
                // Admin u otros roles van al principal
                formToOpen = new FrmPrincipal();
            }

            using (formToOpen)
            {
                formToOpen.ShowDialog(this);
            }

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Botón de depuración: mostrar hash generado y verificar contra hash almacenado
        private void btnShowHash_Click(object sender, EventArgs e)
        {
            try
            {
                var email = txtEmail.Text.Trim();
                var password = txtPassword.Text;
                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Ingrese una contraseña para generar/verificar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var generated = PasswordHasher.CreateHash(password);
                string stored = null;
                bool verifies = false;

                if (!string.IsNullOrEmpty(email))
                {
                    try
                    {
                        var repo = new UsuarioRepository();
                        var u = repo.GetByEmail(email);
                        stored = u?.PasswordHash;
                        if (!string.IsNullOrEmpty(stored))
                        {
                            verifies = PasswordHasher.VerifyHash(password, stored);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo leer usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                var msg = "Generated hash (for debugging):\n" + generated + "\n\n";
                msg += "Stored hash: \n" + (stored ?? "(no user or no stored hash)") + "\n\n";
                msg += "Verify password against stored: " + (verifies ? "OK" : "FAIL") + "\n";
                MessageBox.Show(msg, "Hash debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generando/verificando hash: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}