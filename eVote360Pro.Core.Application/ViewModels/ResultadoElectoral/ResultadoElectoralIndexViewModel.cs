namespace eVote360Pro.Core.Application.ViewModels.ResultadoElectoral
{
    public class ResultadoElectoralIndexViewModel
    {
        public int EleccionId { get; set; }

        public string NombreEleccion { get; set; } = string.Empty;

        public DateTime FechaRealizacion { get; set; }

        public List<ResultadoPorPuestoViewModel> ResultadosPorPuesto { get; set; } = new();
    }
}