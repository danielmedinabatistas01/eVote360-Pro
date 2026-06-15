namespace eVote360Pro.Core.Application.DTOs
{
    public class ResumenEleccionDTO
    {
        public int EleccionId { get; set; }

        public string NombreEleccion { get; set; } = string.Empty;

        public DateTime FechaRealizacion { get; set; }

        public string Estado { get; set; } = string.Empty;

        public int TotalCiudadanosQueVotaron { get; set; }

        // Agregar después de Perla/Daniel:
        // public int TotalPartidosParticipantes { get; set; }
        // public int TotalCandidatosParticipantes { get; set; }
    }
}