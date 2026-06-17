
namespace eVote360Pro.Core.Application.Dtos
{
    public class PartidoPoliticoDto : BasicDto<int>
    {
        public required string Nombre { get; set; }

        public required string Siglas { get; set; }

        public string? Descripcion { get; set; }

        public required string LogoUrl { get; set; }

        public bool EsActivo { get; set; }
    }
}
