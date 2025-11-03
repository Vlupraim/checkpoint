using System;
using System.Configuration;
using System.Windows.Forms;
using Checkpoint.Core.Security;

namespace Checkpoint
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
            // Inicializaciones si hacen falta
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;

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

            // set session
            CurrentSession.UsuarioActual = res.User;
            CurrentSession.Roles = res.Roles;

            this.Hide();
            using (var main = new FrmPrincipal())
            {
                main.ShowDialog(this);
            }

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}