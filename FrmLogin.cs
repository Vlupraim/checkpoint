using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using Checkpoint.Core.Security;

namespace checkpoint
{
    public partial class FrmLogin : BaseForm
    {
        private readonly AuthenticationService _auth = new AuthenticationService();
        private readonly ToolTip _tt = new ToolTip();

        public FrmLogin()
        {
            InitializeComponent();

            // Propiedades visuales/UX para aspecto más profesional
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = true;

            // Teclas rápidas/commit: Enter -> btnLogin, Esc -> btnCancelar
            this.AcceptButton = btnLogin;
            this.CancelButton = btnCancelar;

            // Tooltips rápidos
            _tt.SetToolTip(txtEmail, "Introduce tu email (por ejemplo: admin@local)");
            _tt.SetToolTip(txtPassword, "Introduce tu contraseña. Pulsa Enter para iniciar sesión.");
            _tt.SetToolTip(btnLogin, "Iniciar sesión");
            _tt.SetToolTip(btnCancelar, "Salir de la aplicación");

            // Eventos
            this.Load += FrmLogin_Load;

            // Responder Enter en controles (doble seguridad si AcceptButton no aplica)
            txtPassword.KeyDown += TxtPassword_KeyDown;
            txtEmail.KeyDown += TxtEmail_KeyDown;

            // Checkbox show password (lambda extra, designer también enlaza al handler)
            chkShowPassword.CheckedChanged += (s, e) =>
            {
                try { txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '•'; }
                catch { }
            };

            // Theme toggle reflects persisted setting
            try
            {
                chkLightTheme.Checked = checkpoint.Properties.Settings.Default.IsLightTheme;
            }
            catch { }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            CenterLoginBox();
            this.SizeChanged += (s, ev) => CenterLoginBox();

            // UX: foco en email y seleccionar texto si hay algo
            txtEmail.Focus();
            txtEmail.SelectAll();

            // Si quieres que el campo password no muestre texto (asegúrate en designer)
            try { txtPassword.UseSystemPasswordChar = true; } catch { /* ignore */ }
        }

        private void chkLightTheme_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkLightTheme.Checked) this.UseLightTheme();
                else this.UseDarkTheme();
                checkpoint.Properties.Settings.Default.IsLightTheme = chkLightTheme.Checked;
                checkpoint.Properties.Settings.Default.Save();
            }
            catch { }
        }

        private void CenterLoginBox()
        {
            if (panelLoginBox != null)
            {
                int x = (this.ClientSize.Width - panelLoginBox.Width) /2;
                int y = (this.ClientSize.Height - panelLoginBox.Height) /2;
                panelLoginBox.Location = new System.Drawing.Point(x, y);
            }
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                btnLogin.PerformClick();
            }
        }

        private void TxtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                // Mover foco al password si el email parece válido o vacío
                txtPassword.Focus();
                txtPassword.SelectAll();
            }
        }

        private void SetUiBusy(bool busy)
        {
            // Mostrar spinner/progressbar y deshabilitar inputs
            try
            {
                prgLoading.Visible = busy;
                txtEmail.Enabled = !busy;
                txtPassword.Enabled = !busy;
                btnLogin.Enabled = !busy;
                btnCancelar.Enabled = !busy;
                chkShowPassword.Enabled = !busy;
                chkLightTheme.Enabled = !busy;
            }
            catch { }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text.Trim(); // Corrección de Trim

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(this, "Email y contraseña son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SetUiBusy(true);

                // Si Authenticate es sincrónico, ejecutar en Task.Run para no bloquear UI
                var res = await System.Threading.Tasks.Task.Run(() => _auth.Authenticate(email, password));

                if (!res.Success)
                {
                    MessageBox.Show(this, res.Message, "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Feedback visual: seleccionar password para reintento
                    txtPassword.SelectAll();
                    txtPassword.Focus();
                    return;
                }

                // Establecer la sesión
                CurrentSession.UsuarioActual = res.User;
                CurrentSession.Roles = res.Roles;

                this.Hide();

                // Lógica de Redirección
                Form formToOpen = null;

                // 🎯 Convertir a minúsculas para comparación
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
            finally
            {
                SetUiBusy(false);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Designer wiring requires this handler
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            try { txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '•'; }
            catch { }
        }
    }
}
