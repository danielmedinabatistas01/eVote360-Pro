namespace eVote360Pro.Core.Application.ViewModels.AsignacionDirigente
{
    public class SaveAsignacionDirigenteViewModel
    {
        public int Id { get; set; }

        public required int UsuarioId { get; set; }

        public required int PartidoPoliticoId { get; set; }
    }
}