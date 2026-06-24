namespace eVote360Pro.Core.Application.ViewModels.ProcesoVotacion
{
    public class CandidatoSeleccionViewModel
    {
        public int PuestoElectivoId { get; set; }

        public int CandidatoId { get; set; }

        public string NombreCandidato { get; set; } = string.Empty;

        public string PartidoPolitico { get; set; } = string.Empty;

        public string FotoUrl { get; set; } = string.Empty;
    }
}