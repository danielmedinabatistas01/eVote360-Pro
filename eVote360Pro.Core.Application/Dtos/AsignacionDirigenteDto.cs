
namespace eVote360Pro.Core.Application.Dtos
{
    public class AsignacionDirigenteDto
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public int PartidoPoliticoId { get; set; }

        public string? NombreDirigente { get; set; }

        public string? NombrePartido { get; set; }
    }
}
