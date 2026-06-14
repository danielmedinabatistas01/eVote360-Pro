namespace eVote360Pro.Core.Application.DTOs
{
    public class ResultadoElectoralDTO
    {
        public int EleccionId { get; set; }

        public int PuestoElectivoId { get; set; }

        public int? CandidatoId { get; set; }

        public string NombreCandidato { get; set; } = string.Empty;

        public int CantidadVotos { get; set; }

        public decimal Porcentaje { get; set; }

        public bool EsEmpate { get; set; }
    }
}