
using eVote360Pro.Core.Domain.Common;
using eVote360Pro.Core.Domain.Enums;

namespace eVote360Pro.Core.Domain.Entities
{
<<<<<<< HEAD
    public class Usuario: BaseEntity
=======
    public class Usuario
>>>>>>> eb1b559ac20d4f3cefdcf290b1a50940d224b9b1
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public RolUsuario RolUsuario { get; set; }
        public bool Estado { get; set; } = true;
        public int? PartidoPoliticoId { get; set; }
    }
}