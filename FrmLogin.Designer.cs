using System;
using System.Windows.Forms;
using System.Drawing;

namespace checkpoint
{
    partial class FrmLogin : BaseForm
    {
        // ... (Component declarations)
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel panelLoginBox;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.ProgressBar prgLoading;
        private System.Windows.Forms.CheckBox chkLightTheme; // theme toggle

        // ... (Dispose method)
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelLoginBox = new System.Windows.Forms.Panel();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.prgLoading = new System.Windows.Forms.ProgressBar();
            this.chkLightTheme = new System.Windows.Forms.CheckBox();
            this.panelLoginBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLoginBox
            // 
            this.panelLoginBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(48)))), ((int)(((byte)(54)))));
            this.panelLoginBox.Controls.Add(this.chkLightTheme);
            this.panelLoginBox.Controls.Add(this.lblEmail);
            this.panelLoginBox.Controls.Add(this.txtEmail);
            this.panelLoginBox.Controls.Add(this.lblPassword);
            this.panelLoginBox.Controls.Add(this.txtPassword);
            this.panelLoginBox.Controls.Add(this.chkShowPassword);
            this.panelLoginBox.Controls.Add(this.prgLoading);
            this.panelLoginBox.Controls.Add(this.btnLogin);
            this.panelLoginBox.Controls.Add(this.btnCancelar);
            // Posición estática, la lógica de centrado se mueve al archivo FrmLogin.cs
            this.panelLoginBox.Location = new System.Drawing.Point(150,125);
            this.panelLoginBox.Name = "panelLoginBox";
            this.panelLoginBox.Size = new System.Drawing.Size(400,350);
            this.panelLoginBox.TabIndex =0;
            // 
            // chkLightTheme
            // 
            this.chkLightTheme.AutoSize = true;
            this.chkLightTheme.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(185)))), ((int)(((byte)(190)))));
            this.chkLightTheme.Location = new System.Drawing.Point(300,10);
            this.chkLightTheme.Name = "chkLightTheme";
            this.chkLightTheme.Size = new System.Drawing.Size(90,19);
            this.chkLightTheme.TabIndex =10;
            this.chkLightTheme.Text = "Modo claro";
            this.chkLightTheme.UseVisualStyleBackColor = true;
            this.chkLightTheme.CheckedChanged += new System.EventHandler(this.chkLightTheme_CheckedChanged);
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI",10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.lblEmail.Location = new System.Drawing.Point(40,30);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(44,19);
            this.lblEmail.TabIndex =5;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(66)))), ((int)(((byte)(74)))));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI",11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.txtEmail.Location = new System.Drawing.Point(40,55);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(320,29);
            this.txtEmail.TabIndex =1;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI",10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.lblPassword.Location = new System.Drawing.Point(40,100);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(83,19);
            this.lblPassword.TabIndex =6;
            this.lblPassword.Text = "Contraseña:";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(66)))), ((int)(((byte)(74)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI",11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.txtPassword.Location = new System.Drawing.Point(40,125);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(320,29);
            this.txtPassword.TabIndex =2;
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(185)))), ((int)(((byte)(190)))));
            this.chkShowPassword.Location = new System.Drawing.Point(40,165);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(130,19);
            this.chkShowPassword.TabIndex =8;
            this.chkShowPassword.Text = "Mostrar contraseña";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // 
            // prgLoading
            // 
            this.prgLoading.Location = new System.Drawing.Point(40,190);
            this.prgLoading.Name = "prgLoading";
            this.prgLoading.Size = new System.Drawing.Size(320,8);
            this.prgLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prgLoading.MarqueeAnimationSpeed =30;
            this.prgLoading.Visible = false;
            this.prgLoading.TabIndex =9;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
            this.btnLogin.FlatAppearance.BorderSize =0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI",12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(40,210);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(320,45);
            this.btnLogin.TabIndex =3;
            this.btnLogin.Text = "ACCEDER";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
            this.btnCancelar.FlatAppearance.BorderSize =0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(140,265);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120,36);
            this.btnCancelar.TabIndex =4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 🎯 CORRECCIÓN: Referencia a método estándar
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(28)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(700,600);
            this.Controls.Add(this.panelLoginBox);
            this.Font = new System.Drawing.Font("Segoe UI",9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login - Checkpoint";
            this.Load += new System.EventHandler(this.FrmLogin_Load);

            this.panelLoginBox.ResumeLayout(false);
            this.panelLoginBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion 
    }
}