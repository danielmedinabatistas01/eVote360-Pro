namespace eVote360Pro.Core.Application.ViewModels.HomeAdministrador
{
    public class ResumenEleccionViewModel
    {
        public int EleccionId { get; set; }

        public string NombreEleccion { get; set; } = string.Empty;

        public DateTime FechaRealizacion { get; set; }

        public string Estado { get; set; } = string.Empty;

        public int TotalCiudadanosQueVotaron { get; set; }

        // Agregar después de Perla/Daniel:
        // public int TotalPartidos { get; set; }
        // public int TotalCandidatos { get; set; }
    }
}