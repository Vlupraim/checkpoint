// FrmDetalleProducto.cs (Crear este archivo si no existe, o modificar si ya lo habías iniciado)
using System;
using System.Windows.Forms;
using Checkpoint.Core.Entities;
using Checkpoint.Data.Repositories;

namespace checkpoint
{
    public partial class FrmDetalleProducto : Form
    {
        private readonly ProductoRepository _repo = new ProductoRepository();
        private Guid? _productoId = null;

        public FrmDetalleProducto()
        {
            InitializeComponent();
            this.Text = "Nuevo Producto";
        }

        public FrmDetalleProducto(Guid idProducto) : this()
        {
            _productoId = idProducto;
            this.Text = "Editar Producto";
            CargarProducto(idProducto);
        }

        private void CargarProducto(Guid id)
        {
            try
            {
                var p = _repo.GetById(id);
                if (p != null)
                {
                    txtSku.Text = p.Sku;
                    txtNombre.Text = p.Nombre;
                    txtUnidad.Text = p.Unidad;
                    txtVidaUtilDias.Text = p.VidaUtilDias.ToString();
                    txtStockMinimo.Text = p.StockMinimo.ToString();
                    chkActivo.Checked = p.Activo;
                    // Los campos TempMin/TempMax quedan pendientes, pero la lógica está lista
                }
                else
                {
                    MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                if (_productoId.HasValue)
                {
                    ActualizarProducto(_productoId.Value);
                }
                else
                {
                    CrearNuevoProducto();
                }

                MessageBox.Show("Producto guardado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtSku.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("SKU y Nombre son obligatorios.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // (Agregar aquí más validaciones de formato/tipos)
            return true;
        }

        private void CrearNuevoProducto()
        {
            var producto = new Producto
            {
                Sku = txtSku.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Unidad = txtUnidad.Text.Trim(),
                VidaUtilDias = int.Parse(txtVidaUtilDias.Text), // Asumiendo que es un entero válido
                StockMinimo = decimal.Parse(txtStockMinimo.Text), // Asumiendo que es un decimal válido
                Activo = chkActivo.Checked
            };
            _repo.Insert(producto);
        }

        private void ActualizarProducto(Guid id)
        {
            var producto = new Producto
            {
                Id = id,
                Sku = txtSku.Text.Trim(),
                Nombre = txtNombre.Text.Trim(),
                Unidad = txtUnidad.Text.Trim(),
                VidaUtilDias = int.Parse(txtVidaUtilDias.Text),
                StockMinimo = decimal.Parse(txtStockMinimo.Text),
                Activo = chkActivo.Checked
            };
            _repo.Update(producto);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}