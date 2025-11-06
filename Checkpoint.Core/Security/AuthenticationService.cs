using System;
using Checkpoint.Data.Repositories;
using Checkpoint.Core.Security;
using System.Linq; // Añadido para .ToArray()

namespace Checkpoint.Core.Security
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Checkpoint.Core.Entities.Usuario User { get; set; }
        public string[] Roles { get; set; }
    }

    public class AuthenticationService
    {
        private readonly UsuarioRepository _usuarioRepo = new UsuarioRepository();
        public AuthenticationResult Authenticate(string email, string password)
        {
            var u = _usuarioRepo.GetByEmail(email);
            if (u == null) return new AuthenticationResult { Success = false, Message = "Usuario no encontrado." };
            if (!u.Activo) return new AuthenticationResult { Success = false, Message = "Usuario inactivo." };
            if (string.IsNullOrEmpty(u.PasswordHash)) return new AuthenticationResult { Success = false, Message = "Usuario sin password establecido." };

            // Esta línea llamará al PasswordHasher.cs correcto
            var ok = PasswordHasher.VerifyHash(password, u.PasswordHash);

            if (!ok) return new AuthenticationResult { Success = false, Message = "Credenciales inválidas." };
            var roles = _usuarioRepo.GetRoles(u.Id);
            return new AuthenticationResult { Success = true, Message = "OK", User = u, Roles = roles.ToArray() };
        }
    }
}