using System;

namespace Checkpoint.Core.Entities
{
 public class Ubicacion
 {
 public Guid Id { get; set; }
 public Guid SedeId { get; set; }
 public string Codigo { get; set; }
 public string Tipo { get; set; }
 public decimal? Capacidad { get; set; }
 }
}
