namespace checkpoint
{
 partial class FrmProductos
 {
 /// <summary>
 /// Required designer variable.
 /// </summary>
 private System.ComponentModel.IContainer components = null;
 private System.Windows.Forms.DataGridView dgvProductos;
 private System.Windows.Forms.Button btnNuevo;
 private System.Windows.Forms.Button btnEditar;
 private System.Windows.Forms.Button btnEliminar;
 private System.Windows.Forms.Button btnRefrescar;

 /// <summary>
 /// Clean up any resources being used.
 /// </summary>
 /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
 protected override void Dispose(bool disposing)
 {
 if (disposing && (components != null))
 {
 components.Dispose();
 }
 base.Dispose(disposing);
 }

 #region Windows Form Designer generated code

 /// <summary>
 /// Required method for Designer support - do not modify
 /// the contents of this method with the code editor.
 /// </summary>
 private void InitializeComponent()
 {
 this.dgvProductos = new System.Windows.Forms.DataGridView();
 this.btnNuevo = new System.Windows.Forms.Button();
 this.btnEditar = new System.Windows.Forms.Button();
 this.btnEliminar = new System.Windows.Forms.Button();
 this.btnRefrescar = new System.Windows.Forms.Button();
 ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
 this.SuspendLayout();
 // 
 // dgvProductos
 // 
 this.dgvProductos.Location = new System.Drawing.Point(12,12);
 this.dgvProductos.Name = "dgvProductos";
 this.dgvProductos.Size = new System.Drawing.Size(760,360);
 this.dgvProductos.ReadOnly = true;
 this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
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
 // btnEditar
 // 
 this.btnEditar.Location = new System.Drawing.Point(122,380);
 this.btnEditar.Name = "btnEditar";
 this.btnEditar.Size = new System.Drawing.Size(100,30);
 this.btnEditar.TabIndex =2;
 this.btnEditar.Text = "Editar";
 this.btnEditar.UseVisualStyleBackColor = true;
 this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
 // 
 // btnEliminar
 // 
 this.btnEliminar.Location = new System.Drawing.Point(232,380);
 this.btnEliminar.Name = "btnEliminar";
 this.btnEliminar.Size = new System.Drawing.Size(100,30);
 this.btnEliminar.TabIndex =3;
 this.btnEliminar.Text = "Eliminar";
 this.btnEliminar.UseVisualStyleBackColor = true;
 this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
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
 // FrmProductos
 // 
 this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
 this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
 this.ClientSize = new System.Drawing.Size(784,421);
 this.Controls.Add(this.dgvProductos);
 this.Controls.Add(this.btnNuevo);
 this.Controls.Add(this.btnEditar);
 this.Controls.Add(this.btnEliminar);
 this.Controls.Add(this.btnRefrescar);
 this.Name = "FrmProductos";
 this.Text = "Productos";
 ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
 this.ResumeLayout(false);
 }

 #endregion
 }
}
