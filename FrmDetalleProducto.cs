// FrmDetalleProducto.cs
using System;
using System.Globalization;
using System.Windows.Forms;
using Checkpoint.Core.Entities;
using Checkpoint.Data.Repositories;

namespace checkpoint
{
    public partial class FrmDetalleProducto : BaseForm
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
                if (p == null)
                {
                    MessageBox.Show("Producto no encontrado.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                txtSku.Text = p.Sku;
                txtNombre.Text = p.Nombre;
                txtUnidad.Text = p.Unidad;
                txtVidaUtilDias.Text = p.VidaUtilDias.ToString();
                txtStockMinimo.Text = p.StockMinimo.ToString();
                txtTempMin.Text = p.TempMin.HasValue ? p.TempMin.Value.ToString(CultureInfo.CurrentCulture) : "";
                txtTempMax.Text = p.TempMax.HasValue ? p.TempMax.Value.ToString(CultureInfo.CurrentCulture) : "";
                chkActivo.Checked = p.Activo;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando producto: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // ----- Validaciones mínimas
                if (string.IsNullOrWhiteSpace(txtSku.Text))
                    throw new ArgumentException("El SKU es obligatorio.");

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                    throw new ArgumentException("El nombre es obligatorio.");

                // Vida útil y stock como enteros
                int vidaUtil = 0;
                if (!string.IsNullOrWhiteSpace(txtVidaUtilDias.Text) &&
                    !int.TryParse(txtVidaUtilDias.Text.Trim(), out vidaUtil))
                    throw new ArgumentException("Vida útil debe ser un número entero.");

                int stockMin = 0;
                if (!string.IsNullOrWhiteSpace(txtStockMinimo.Text) &&
                    !int.TryParse(txtStockMinimo.Text.Trim(), out stockMin))
                    throw new ArgumentException("Stock mínimo debe ser un número entero.");

                // Temperaturas como decimal (acepta coma o punto según cultura)
                decimal? tempMin = null, tempMax = null;
                decimal tempParsed;

                if (!string.IsNullOrWhiteSpace(txtTempMin.Text))
                {
                    if (!decimal.TryParse(txtTempMin.Text.Trim(), NumberStyles.Any, CultureInfo.CurrentCulture, out tempParsed))
                        throw new ArgumentException("Temperatura mínima inválida.");
                    tempMin = tempParsed;
                }

                if (!string.IsNullOrWhiteSpace(txtTempMax.Text))
                {
                    if (!decimal.TryParse(txtTempMax.Text.Trim(), NumberStyles.Any, CultureInfo.CurrentCulture, out tempParsed))
                        throw new ArgumentException("Temperatura máxima inválida.");
                    tempMax = tempParsed;
                }

                // ----- Mapear entidad
                var producto = new Producto
                {
                    Id = _productoId ?? Guid.Empty, // el repo suele generar si es Empty
                    Sku = txtSku.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Unidad = txtUnidad.Text.Trim(),
                    VidaUtilDias = vidaUtil,
                    StockMinimo = stockMin,
                    TempMin = tempMin,
                    TempMax = tempMax,
                    Activo = chkActivo.Checked
                };

                // ----- Insert / Update
                if (_productoId.HasValue)
                    _repo.Update(producto);
                else
                    _repo.Insert(producto);

                DialogResult = DialogResult.OK;
            }
            catch (ArgumentException ax)
            {
                MessageBox.Show(ax.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
