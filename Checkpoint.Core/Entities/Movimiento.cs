using System;

namespace Checkpoint.Core.Entities
{
 public class Movimiento
 {
 public Guid Id { get; set; }
 public Guid LoteId { get; set; }
 public Guid SedeId { get; set; }
 public Guid? OrigenUbicacionId { get; set; }
 public Guid? DestinoUbicacionId { get; set; }
 public string Tipo { get; set; }
 public decimal Cantidad { get; set; }
 public string Unidad { get; set; }
 public DateTime Fecha { get; set; }
 public Guid? UsuarioId { get; set; }
 public string Motivo { get; set; }
 }
}
