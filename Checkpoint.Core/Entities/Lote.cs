using System;

namespace Checkpoint.Core.Entities
{
 public class Lote
 {
 public Guid Id { get; set; }
 public Guid ProductoId { get; set; }
 public string CodigoLote { get; set; }
 public DateTime FechaIngreso { get; set; }
 public DateTime? FechaVencimiento { get; set; }
 public string OrdenCompra { get; set; }
 public string GuiaRecepcion { get; set; }
 public decimal? TempIngreso { get; set; }
 public string Estado { get; set; } // Pendiente, Liberado, Bloqueado
 }
}
