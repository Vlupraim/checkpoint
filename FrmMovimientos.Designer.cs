namespace checkpoint
{
 partial class FrmMovimientos
 {
 private System.ComponentModel.IContainer components = null;
 private System.Windows.Forms.TabControl tabControl;
 private System.Windows.Forms.TabPage tabIngreso;
 private System.Windows.Forms.TabPage tabSalida;
 private System.Windows.Forms.TabPage tabMovimiento;
 private System.Windows.Forms.TabPage tabDevolucion;
 private System.Windows.Forms.TabPage tabAjuste;
 private System.Windows.Forms.Button btnRegistrarIngreso;
 private System.Windows.Forms.Button btnRegistrarSalida;
 private System.Windows.Forms.Button btnRegistrarMovimiento;
 private System.Windows.Forms.Button btnRegistrarDevolucion;
 private System.Windows.Forms.Button btnRegistrarAjuste;

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
 this.tabControl = new System.Windows.Forms.TabControl();
 this.tabIngreso = new System.Windows.Forms.TabPage();
 this.tabSalida = new System.Windows.Forms.TabPage();
 this.tabMovimiento = new System.Windows.Forms.TabPage();
 this.tabDevolucion = new System.Windows.Forms.TabPage();
 this.tabAjuste = new System.Windows.Forms.TabPage();
 this.btnRegistrarIngreso = new System.Windows.Forms.Button();
 this.btnRegistrarSalida = new System.Windows.Forms.Button();
 this.btnRegistrarMovimiento = new System.Windows.Forms.Button();
 this.btnRegistrarDevolucion = new System.Windows.Forms.Button();
 this.btnRegistrarAjuste = new System.Windows.Forms.Button();
 this.SuspendLayout();
 // 
 // tabControl
 // 
 this.tabControl.Location = new System.Drawing.Point(12,12);
 this.tabControl.Name = "tabControl";
 this.tabControl.Size = new System.Drawing.Size(760,360);
 this.tabControl.Controls.Add(this.tabIngreso);
 this.tabControl.Controls.Add(this.tabSalida);
 this.tabControl.Controls.Add(this.tabMovimiento);
 this.tabControl.Controls.Add(this.tabDevolucion);
 this.tabControl.Controls.Add(this.tabAjuste);
 // 
 // tabIngreso
 // 
 this.tabIngreso.Text = "Ingreso";
 // 
 // tabSalida
 // 
 this.tabSalida.Text = "Salida";
 // 
 // tabMovimiento
 // 
 this.tabMovimiento.Text = "Movimiento interno";
 // 
 // tabDevolucion
 // 
 this.tabDevolucion.Text = "Devolución";
 // 
 // tabAjuste
 // 
 this.tabAjuste.Text = "Ajuste";
 // 
 // btnRegistrarIngreso
 // 
 this.btnRegistrarIngreso.Location = new System.Drawing.Point(12,388);
 this.btnRegistrarIngreso.Name = "btnRegistrarIngreso";
 this.btnRegistrarIngreso.Size = new System.Drawing.Size(140,30);
 this.btnRegistrarIngreso.Text = "Registrar Ingreso";
 this.btnRegistrarIngreso.UseVisualStyleBackColor = true;
 this.btnRegistrarIngreso.Click += new System.EventHandler(this.btnRegistrarIngreso_Click);
 // 
 // btnRegistrarSalida
 // 
 this.btnRegistrarSalida.Location = new System.Drawing.Point(162,388);
 this.btnRegistrarSalida.Name = "btnRegistrarSalida";
 this.btnRegistrarSalida.Size = new System.Drawing.Size(140,30);
 this.btnRegistrarSalida.Text = "Registrar Salida";
 this.btnRegistrarSalida.UseVisualStyleBackColor = true;
 this.btnRegistrarSalida.Click += new System.EventHandler(this.btnRegistrarSalida_Click);
 // 
 // btnRegistrarMovimiento
 // 
 this.btnRegistrarMovimiento.Location = new System.Drawing.Point(312,388);
 this.btnRegistrarMovimiento.Name = "btnRegistrarMovimiento";
 this.btnRegistrarMovimiento.Size = new System.Drawing.Size(160,30);
 this.btnRegistrarMovimiento.Text = "Registrar Movimiento";
 this.btnRegistrarMovimiento.UseVisualStyleBackColor = true;
 this.btnRegistrarMovimiento.Click += new System.EventHandler(this.btnRegistrarMovimiento_Click);
 // 
 // btnRegistrarDevolucion
 // 
 this.btnRegistrarDevolucion.Location = new System.Drawing.Point(482,388);
 this.btnRegistrarDevolucion.Name = "btnRegistrarDevolucion";
 this.btnRegistrarDevolucion.Size = new System.Drawing.Size(140,30);
 this.btnRegistrarDevolucion.Text = "Registrar Devolución";
 this.btnRegistrarDevolucion.UseVisualStyleBackColor = true;
 this.btnRegistrarDevolucion.Click += new System.EventHandler(this.btnRegistrarDevolucion_Click);
 // 
 // btnRegistrarAjuste
 // 
 this.btnRegistrarAjuste.Location = new System.Drawing.Point(632,388);
 this.btnRegistrarAjuste.Name = "btnRegistrarAjuste";
 this.btnRegistrarAjuste.Size = new System.Drawing.Size(140,30);
 this.btnRegistrarAjuste.Text = "Registrar Ajuste";
 this.btnRegistrarAjuste.UseVisualStyleBackColor = true;
 this.btnRegistrarAjuste.Click += new System.EventHandler(this.btnRegistrarAjuste_Click);
 // 
 // FrmMovimientos
 // 
 this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
 this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
 this.ClientSize = new System.Drawing.Size(784,421);
 this.Controls.Add(this.tabControl);
 this.Controls.Add(this.btnRegistrarIngreso);
 this.Controls.Add(this.btnRegistrarSalida);
 this.Controls.Add(this.btnRegistrarMovimiento);
 this.Controls.Add(this.btnRegistrarDevolucion);
 this.Controls.Add(this.btnRegistrarAjuste);
 this.Name = "FrmMovimientos";
 this.Text = "Movimientos";
 this.ResumeLayout(false);
 }
 }
}
