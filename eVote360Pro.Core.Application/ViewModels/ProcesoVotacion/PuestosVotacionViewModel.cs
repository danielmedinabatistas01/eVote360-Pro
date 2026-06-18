namespace eVote360Pro.Core.Application.ViewModels.ProcesoVotacion
{
    public class PuestosVotacionViewModel
    {
        public string NombreEleccion { get; set; } = string.Empty;

        public List<PuestoDisponibleViewModel> Puestos { get; set; } = new();

        public bool PuedeFinalizar => Puestos.Any() && Puestos.All(p => p.YaSeleccionado);
    }
}