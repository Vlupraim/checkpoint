namespace checkpoint
{
 partial class FrmReportes
 {
 private System.ComponentModel.IContainer components = null;
 private System.Windows.Forms.DataGridView dgvReport;
 private System.Windows.Forms.Button btnStockPorUbicacion;

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
 this.dgvReport = new System.Windows.Forms.DataGridView();
 this.btnStockPorUbicacion = new System.Windows.Forms.Button();
 ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
 this.SuspendLayout();
 // 
 // dgvReport
 // 
 this.dgvReport.Location = new System.Drawing.Point(12,12);
 this.dgvReport.Name = "dgvReport";
 this.dgvReport.Size = new System.Drawing.Size(760,360);
 this.dgvReport.ReadOnly = true;
 // 
 // btnStockPorUbicacion
 // 
 this.btnStockPorUbicacion.Location = new System.Drawing.Point(12,388);
 this.btnStockPorUbicacion.Name = "btnStockPorUbicacion";
 this.btnStockPorUbicacion.Size = new System.Drawing.Size(200,30);
 this.btnStockPorUbicacion.Text = "Stock por Ubicación";
 this.btnStockPorUbicacion.UseVisualStyleBackColor = true;
 this.btnStockPorUbicacion.Click += new System.EventHandler(this.btnStockPorUbicacion_Click);
 // 
 // FrmReportes
 // 
 this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
 this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
 this.ClientSize = new System.Drawing.Size(784,421);
 this.Controls.Add(this.dgvReport);
 this.Controls.Add(this.btnStockPorUbicacion);
 this.Name = "FrmReportes";
 this.Text = "Reportes";
 ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
 this.ResumeLayout(false);
 }
 }
}
