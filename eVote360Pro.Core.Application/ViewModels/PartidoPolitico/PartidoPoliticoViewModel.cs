namespace eVote360Pro.Core.Application.ViewModels.PartidoPolitico
{
    public class PartidoPoliticoViewModel
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public required string Siglas { get; set; }

        public string? Descripcion { get; set; }

        public required string LogoUrl { get; set; }

        public required bool EsActivo { get; set; }
    }
}