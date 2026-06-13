namespace eVote360Pro.Core.Application.ViewModels.HomeAdministrador
{
    public class HomeAdministradorViewModel
    {
        public List<int> AniosDisponibles { get; set; } = [];

        public int? AnioSeleccionado { get; set; }

        public List<ResumenEleccionViewModel> Resumenes { get; set; } = [];
    }
}