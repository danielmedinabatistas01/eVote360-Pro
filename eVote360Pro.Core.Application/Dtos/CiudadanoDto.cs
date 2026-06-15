
namespace eVote360Pro.Core.Application.Dtos
{
    public class CiudadanoDto
    {
        public required string Nombre { get; set; }

        public required string Apellido { get; set; }

        public required string DocumentoIdentidad { get; set; }

        public required string CorreoElectronico { get; set; }
    }
}
