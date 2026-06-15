namespace eVote360Pro.Core.Application.ViewModels.ResultadoElectoral
{
    public class ResultadoPorPuestoViewModel
    {
        public int PuestoElectivoId { get; set; }

        public string NombrePuesto { get; set; } = string.Empty;

        public int? CandidatoId { get; set; }

        public string NombreCandidato { get; set; } = string.Empty;

        public int CantidadVotos { get; set; }

        public decimal Porcentaje { get; set; }

        public bool EsEmpate { get; set; }
    }
}