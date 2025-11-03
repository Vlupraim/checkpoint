using System;

namespace Checkpoint.Core.Entities
{
 public class Usuario
 {
 public Guid Id { get; set; }
 public string Nombre { get; set; }
 public string Email { get; set; }
 public string PasswordHash { get; set; }
 public bool Activo { get; set; }
 }
}
