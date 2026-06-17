namespace eVote360Pro.Core.Application.ViewModels.AsignacionDirigente
{
    public class SaveAsignacionDirigenteViewModel: BasicViewModel<int>
    {

        public int UsuarioId { get; set; } = 0;

        public int PartidoPoliticoId { get; set; } = 0; 
    }
}