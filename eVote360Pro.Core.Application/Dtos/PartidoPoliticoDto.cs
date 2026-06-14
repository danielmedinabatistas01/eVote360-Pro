
namespace eVote360Pro.Core.Application.Dtos
{
    public class PartidoPoliticoDto
    {
        public required string Nombre { get; set; }

        public required string Siglas { get; set; }

        public string? Descripcion { get; set; }

        public required string LogoUrl { get; set; }
    }
}
