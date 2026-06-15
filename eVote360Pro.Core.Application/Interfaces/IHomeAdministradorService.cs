using eVote360Pro.Core.Application.ViewModels.HomeAdministrador;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IHomeAdministradorService
    {
        Task<HomeAdministradorViewModel> GetResumenByAnioAsync(int anio);
    }
}