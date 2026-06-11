
using eVote360Pro.Core.Domain.Enums;

namespace eVote360Pro.Core.Application.ViewModels.Usuario
{
    public class UsuarioIndexViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string NombreCompleto => $"{Nombre} {Apellido}";

        public string CorreoElectronico { get; set; } = string.Empty;

        public string NombreUsuario { get; set; } = string.Empty;

        public RolUsuario RolUsuario { get; set; }

        public bool Estado { get; set; }

        public int? PartidoPoliticoId { get; set; }
    }
}