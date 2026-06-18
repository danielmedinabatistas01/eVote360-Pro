namespace eVote360Pro.Core.Application.ViewModels.ProcesoVotacion
{
    public class CandidatoSeleccionViewModel
    {
        public int? CandidatoId { get; set; }

        public string NombreCandidato { get; set; } = string.Empty;

        public string NombrePartido { get; set; } = string.Empty;

        public string? FotoCandidatoUrl { get; set; }

        public string? LogoPartidoUrl { get; set; }

        public bool EsNinguno { get; set; }
    }
}