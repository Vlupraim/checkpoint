using System;
using System.Windows.Forms;
using Checkpoint.Core.Security;

namespace checkpoint
{
 partial class FrmLogin : Form
 {
 private System.ComponentModel.IContainer components = null;
 private TextBox txtEmail;
 private TextBox txtPassword;
 private Button btnLogin;
 private Button btnCancelar;
 private Label lblEmail;
 private Label lblPassword;

 protected override void Dispose(bool disposing)
 {
 if (disposing && (components != null))
 {
 components.Dispose();
 }
 base.Dispose(disposing);
 }

 private void InitializeComponent()
 {
 this.txtEmail = new System.Windows.Forms.TextBox();
 this.txtPassword = new System.Windows.Forms.TextBox();
 this.btnLogin = new System.Windows.Forms.Button();
 this.btnCancelar = new System.Windows.Forms.Button();
 this.lblEmail = new System.Windows.Forms.Label();
 this.lblPassword = new System.Windows.Forms.Label();
 this.SuspendLayout();
 // 
 // lblEmail
 // 
 this.lblEmail.AutoSize = true;
 this.lblEmail.Location = new System.Drawing.Point(22,20);
 this.lblEmail.Name = "lblEmail";
 this.lblEmail.Size = new System.Drawing.Size(36,13);
 this.lblEmail.Text = "Email:";
 // 
 // txtEmail
 // 
 this.txtEmail.Location = new System.Drawing.Point(22,40);
 this.txtEmail.Name = "txtEmail";
 this.txtEmail.Size = new System.Drawing.Size(260,20);
 // 
 // lblPassword
 // 
 this.lblPassword.AutoSize = true;
 this.lblPassword.Location = new System.Drawing.Point(22,70);
 this.lblPassword.Name = "lblPassword";
 this.lblPassword.Size = new System.Drawing.Size(64,13);
 this.lblPassword.Text = "Contraseña:";
 // 
 // txtPassword
 // 
 this.txtPassword.Location = new System.Drawing.Point(22,90);
 this.txtPassword.Name = "txtPassword";
 this.txtPassword.PasswordChar = '*';
 this.txtPassword.Size = new System.Drawing.Size(260,20);
 // 
 // btnLogin
 // 
 this.btnLogin.Location = new System.Drawing.Point(22,130);
 this.btnLogin.Name = "btnLogin";
 this.btnLogin.Size = new System.Drawing.Size(120,30);
 this.btnLogin.Text = "Acceder";
 this.btnLogin.UseVisualStyleBackColor = true;
 this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
 // 
 // btnCancelar
 // 
 this.btnCancelar.Location = new System.Drawing.Point(162,130);
 this.btnCancelar.Name = "btnCancelar";
 this.btnCancelar.Size = new System.Drawing.Size(120,30);
 this.btnCancelar.Text = "Cancelar";
 this.btnCancelar.UseVisualStyleBackColor = true;
 this.btnCancelar.Click += new System.EventHandler((s, e) => Application.Exit());
 // 
 // FrmLogin
 // 
 this.ClientSize = new System.Drawing.Size(310,185);
 this.Controls.Add(this.lblEmail);
 this.Controls.Add(this.txtEmail);
 this.Controls.Add(this.lblPassword);
 this.Controls.Add(this.txtPassword);
 this.Controls.Add(this.btnLogin);
 this.Controls.Add(this.btnCancelar);
 this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
 this.MaximizeBox = false;
 this.MinimizeBox = false;
 this.Name = "FrmLogin";
 this.Text = "Login - Checkpoint";
 this.ResumeLayout(false);
 this.PerformLayout();
 }

 // Event handler implemented here to ensure the class compiles when only designer file is included in project
 private void btnLogin_Click(object sender, EventArgs e)
 {
 var email = txtEmail.Text.Trim();
 var password = txtPassword.Text;
 if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
 {
 MessageBox.Show("Email y contraseña son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
 return;
 }
 var auth = new AuthenticationService();
 var res = auth.Authenticate(email, password);
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
 }
}
