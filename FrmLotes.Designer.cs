namespace checkpoint
{
 partial class FrmLotes
 {
 private System.ComponentModel.IContainer components = null;
 private System.Windows.Forms.DataGridView dgvLotes;
 private System.Windows.Forms.Button btnNuevo;
 private System.Windows.Forms.Button btnLiberar;
 private System.Windows.Forms.Button btnBloquear;
 private System.Windows.Forms.Button btnRefrescar;

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
 this.dgvLotes = new System.Windows.Forms.DataGridView();
 this.btnNuevo = new System.Windows.Forms.Button();
 this.btnLiberar = new System.Windows.Forms.Button();
 this.btnBloquear = new System.Windows.Forms.Button();
 this.btnRefrescar = new System.Windows.Forms.Button();
 ((System.ComponentModel.ISupportInitialize)(this.dgvLotes)).BeginInit();
 this.SuspendLayout();
 // 
 // dgvLotes
 // 
 this.dgvLotes.Location = new System.Drawing.Point(12,12);
 this.dgvLotes.Name = "dgvLotes";
 this.dgvLotes.Size = new System.Drawing.Size(760,360);
 this.dgvLotes.ReadOnly = true;
 this.dgvLotes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
 // 
 // btnNuevo
 // 
 this.btnNuevo.Location = new System.Drawing.Point(12,380);
 this.btnNuevo.Name = "btnNuevo";
 this.btnNuevo.Size = new System.Drawing.Size(100,30);
 this.btnNuevo.TabIndex =1;
 this.btnNuevo.Text = "Nuevo";
 this.btnNuevo.UseVisualStyleBackColor = true;
 this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
 // 
 // btnLiberar
 // 
 this.btnLiberar.Location = new System.Drawing.Point(122,380);
 this.btnLiberar.Name = "btnLiberar";
 this.btnLiberar.Size = new System.Drawing.Size(110,30);
 this.btnLiberar.TabIndex =2;
 this.btnLiberar.Text = "Liberar";
 this.btnLiberar.UseVisualStyleBackColor = true;
 this.btnLiberar.Click += new System.EventHandler(this.btnLiberar_Click);
 // 
 // btnBloquear
 // 
 this.btnBloquear.Location = new System.Drawing.Point(242,380);
 this.btnBloquear.Name = "btnBloquear";
 this.btnBloquear.Size = new System.Drawing.Size(110,30);
 this.btnBloquear.TabIndex =3;
 this.btnBloquear.Text = "Bloquear";
 this.btnBloquear.UseVisualStyleBackColor = true;
 this.btnBloquear.Click += new System.EventHandler(this.btnBloquear_Click);
 // 
 // btnRefrescar
 // 
 this.btnRefrescar.Location = new System.Drawing.Point(672,380);
 this.btnRefrescar.Name = "btnRefrescar";
 this.btnRefrescar.Size = new System.Drawing.Size(100,30);
 this.btnRefrescar.TabIndex =4;
 this.btnRefrescar.Text = "Refrescar";
 this.btnRefrescar.UseVisualStyleBackColor = true;
 this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
 // 
 // FrmLotes
 // 
 this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
 this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
 this.ClientSize = new System.Drawing.Size(784,421);
 this.Controls.Add(this.dgvLotes);
 this.Controls.Add(this.btnNuevo);
 this.Controls.Add(this.btnLiberar);
 this.Controls.Add(this.btnBloquear);
 this.Controls.Add(this.btnRefrescar);
 this.Name = "FrmLotes";
 this.Text = "Lotes";
 ((System.ComponentModel.ISupportInitialize)(this.dgvLotes)).EndInit();
 this.ResumeLayout(false);
 }
 }
}
