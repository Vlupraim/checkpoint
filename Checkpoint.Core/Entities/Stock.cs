using System;

namespace Checkpoint.Core.Entities
{
 public class Stock
 {
 public Guid Id { get; set; }
 public Guid LoteId { get; set; }
 public Guid UbicacionId { get; set; }
 public decimal Cantidad { get; set; }
 public string Unidad { get; set; }
 public DateTime ActualizadoEn { get; set; }
 }
}
