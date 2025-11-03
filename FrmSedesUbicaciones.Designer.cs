namespace checkpoint
{
 partial class FrmSedesUbicaciones
 {
 private System.ComponentModel.IContainer components = null;
 private System.Windows.Forms.DataGridView dgvSedes;
 private System.Windows.Forms.DataGridView dgvUbicaciones;
 private System.Windows.Forms.Button btnNuevaSede;
 private System.Windows.Forms.Button btnNuevaUbicacion;
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
 this.dgvSedes = new System.Windows.Forms.DataGridView();
 this.dgvUbicaciones = new System.Windows.Forms.DataGridView();
 this.btnNuevaSede = new System.Windows.Forms.Button();
 this.btnNuevaUbicacion = new System.Windows.Forms.Button();
 this.btnRefrescar = new System.Windows.Forms.Button();
 ((System.ComponentModel.ISupportInitialize)(this.dgvSedes)).BeginInit();
 ((System.ComponentModel.ISupportInitialize)(this.dgvUbicaciones)).BeginInit();
 this.SuspendLayout();
 // 
 // dgvSedes
 // 
 this.dgvSedes.Location = new System.Drawing.Point(12,12);
 this.dgvSedes.Name = "dgvSedes";
 this.dgvSedes.Size = new System.Drawing.Size(360,360);
 this.dgvSedes.ReadOnly = true;
 this.dgvSedes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
 // 
 // dgvUbicaciones
 // 
 this.dgvUbicaciones.Location = new System.Drawing.Point(392,12);
 this.dgvUbicaciones.Name = "dgvUbicaciones";
 this.dgvUbicaciones.Size = new System.Drawing.Size(380,360);
 this.dgvUbicaciones.ReadOnly = true;
 this.dgvUbicaciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
 // 
 // btnNuevaSede
 // 
 this.btnNuevaSede.Location = new System.Drawing.Point(12,388);
 this.btnNuevaSede.Name = "btnNuevaSede";
 this.btnNuevaSede.Size = new System.Drawing.Size(120,30);
 this.btnNuevaSede.Text = "Nueva Sede";
 this.btnNuevaSede.UseVisualStyleBackColor = true;
 this.btnNuevaSede.Click += new System.EventHandler(this.btnNuevaSede_Click);
 // 
 // btnNuevaUbicacion
 // 
 this.btnNuevaUbicacion.Location = new System.Drawing.Point(392,388);
 this.btnNuevaUbicacion.Name = "btnNuevaUbicacion";
 this.btnNuevaUbicacion.Size = new System.Drawing.Size(140,30);
 this.btnNuevaUbicacion.Text = "Nueva Ubicación";
 this.btnNuevaUbicacion.UseVisualStyleBackColor = true;
 this.btnNuevaUbicacion.Click += new System.EventHandler(this.btnNuevaUbicacion_Click);
 // 
 // btnRefrescar
 // 
 this.btnRefrescar.Location = new System.Drawing.Point(652,388);
 this.btnRefrescar.Name = "btnRefrescar";
 this.btnRefrescar.Size = new System.Drawing.Size(120,30);
 this.btnRefrescar.Text = "Refrescar";
 this.btnRefrescar.UseVisualStyleBackColor = true;
 this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
 // 
 // FrmSedesUbicaciones
 // 
 this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
 this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
 this.ClientSize = new System.Drawing.Size(784,421);
 this.Controls.Add(this.dgvSedes);
 this.Controls.Add(this.dgvUbicaciones);
 this.Controls.Add(this.btnNuevaSede);
 this.Controls.Add(this.btnNuevaUbicacion);
 this.Controls.Add(this.btnRefrescar);
 this.Name = "FrmSedesUbicaciones";
 this.Text = "Sedes y Ubicaciones";
 ((System.ComponentModel.ISupportInitialize)(this.dgvSedes)).EndInit();
 ((System.ComponentModel.ISupportInitialize)(this.dgvUbicaciones)).EndInit();
 this.ResumeLayout(false);
 }
 }
}
