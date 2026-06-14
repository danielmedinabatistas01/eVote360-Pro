namespace eVote360Pro.Core.Application.ViewModels.AsignacionDirigente
{
    public class AsignacionDirigenteViewModel
    {
        public int Id { get; set; }

        public required int UsuarioId { get; set; }

        public required string NombreDirigente { get; set; }

        public required int PartidoPoliticoId { get; set; }

        public required string NombrePartido { get; set; }
    }
}