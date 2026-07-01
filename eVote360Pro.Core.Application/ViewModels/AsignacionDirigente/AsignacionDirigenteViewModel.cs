namespace eVote360Pro.Core.Application.ViewModels.AsignacionDirigente
{
    public class AsignacionDirigenteViewModel : BasicViewModel<int>
    {
        public  int UsuarioId { get; set; }
        public  string NombreDirigente { get; set; }
        public int PartidoPoliticoId { get; set; }
        public  string NombrePartido { get; set; }
    }
}