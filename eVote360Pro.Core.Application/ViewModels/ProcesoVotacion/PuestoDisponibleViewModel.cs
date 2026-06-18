namespace eVote360Pro.Core.Application.ViewModels.ProcesoVotacion
{
    public class PuestoDisponibleViewModel
    {
        public int PuestoElectivoId { get; set; }

        public string NombrePuesto { get; set; } = string.Empty;

        public int CantidadPartidosParticipantes { get; set; }

        public int CantidadCandidatosReales { get; set; }

        public bool YaSeleccionado { get; set; }

        public string EstadoSeleccion => YaSeleccionado ? "Seleccionado" : "Pendiente";
    }
}