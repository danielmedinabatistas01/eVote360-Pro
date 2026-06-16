namespace eVote360Pro.Core.Application.ViewModels.AsignacionDirigente
{
    public class SaveAsignacionDirigenteViewModel: BasicViewModel<int>
    {

        public required int UsuarioId { get; set; }

        public required int PartidoPoliticoId { get; set; }
    }
}