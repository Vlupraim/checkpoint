using System;

namespace Checkpoint.Core.Entities
{
    public class Rol
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }

        // (Puedes añadir más propiedades si las necesitas, 
        // pero Id y Nombre son las requeridas por el repositorio)
    }
}