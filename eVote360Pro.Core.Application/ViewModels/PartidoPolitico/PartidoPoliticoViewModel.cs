namespace eVote360Pro.Core.Application.ViewModels.PartidoPolitico
{
    public class PartidoPoliticoViewModel : BasicViewModel<int>
    {
        public string Nombre { get; set; } = null!;

        public string Siglas { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string? LogoUrl { get; set; }

        public bool EsActivo { get; set; }
    }
}