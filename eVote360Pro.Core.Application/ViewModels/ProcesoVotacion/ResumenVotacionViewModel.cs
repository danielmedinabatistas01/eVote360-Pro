namespace eVote360Pro.Core.Application.ViewModels.ProcesoVotacion
{
    public class ResumenVotacionViewModel
    {
        public string NombreEleccion { get; set; } = string.Empty;

        public DateTime FechaEleccion { get; set; }

        public List<ResumenSeleccionViewModel> Selecciones { get; set; } = new();
    }

    public class ResumenSeleccionViewModel
    {
        public string NombrePuesto { get; set; } = string.Empty;

        public string SeleccionRealizada { get; set; } = string.Empty;

        public string? PartidoPolitico { get; set; }
    }
}