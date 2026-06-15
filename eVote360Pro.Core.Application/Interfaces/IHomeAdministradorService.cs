using eVote360Pro.Core.Application.DTOs;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IHomeAdministradorService
    {
        Task<HomeAdministradorDTO> GetResumenByAnioAsync(int anio);
    }
}