using Checkpoint.Core.Entities; // Necesario para la entidad Usuario

namespace Checkpoint.Core.Security
{
    /// <summary>
    /// Almacena la información de la sesión del usuario actual de forma estática.
    /// </summary>
    public static class CurrentSession
    {
        public static Usuario UsuarioActual { get; set; }
        public static string[] Roles { get; set; }
    }
}