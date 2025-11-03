using System;

namespace Checkpoint.Core.Entities
{
 // POCO de Producto según el modelo simplificado
 public class Producto
 {
 public Guid Id { get; set; }
 public string Sku { get; set; }
 public string Nombre { get; set; }
 public string Unidad { get; set; }
 public int VidaUtilDias { get; set; }
 public decimal? TempMin { get; set; }
 public decimal? TempMax { get; set; }
 public decimal StockMinimo { get; set; }
 public bool Activo { get; set; }
 }
}
