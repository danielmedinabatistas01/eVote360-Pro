namespace eVote360Pro.Core.Application.ViewModels.AsignacionDirigente
{
    public class AsignacionDirigenteViewModel : BasicViewModel<int>
    {

        public required int UsuarioId { get; set; }

        public required string NombreDirigente { get; set; }

        public required int PartidoPoliticoId { get; set; }

        public required string NombrePartido { get; set; }
    }
}