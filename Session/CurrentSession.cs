using System;
using System.Collections.Generic;

namespace checkpoint
{
 public static class CurrentSession
 {
 public static Checkpoint.Core.Entities.Usuario UsuarioActual { get; set; }
 public static IEnumerable<string> Roles { get; set; }

 public static bool IsInRole(string role)
 {
 if (Roles == null) return false;
 foreach (var r in Roles) if (string.Equals(r, role, StringComparison.OrdinalIgnoreCase)) return true;
 return false;
 }
 }
}
