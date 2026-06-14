namespace eVote360Pro.Core.Application.ViewModels.HomeAdministrador
{
    public class ResumenEleccionViewModel
    {
        public int EleccionId { get; set; }

        public string NombreEleccion { get; set; } = string.Empty;

        public DateTime FechaRealizacion { get; set; }

        public int CantidadPartidosParticipantes { get; set; }

        public int CantidadCandidatosParticipantes { get; set; }

        public int CantidadCiudadanosVotaron { get; set; }
    }
}