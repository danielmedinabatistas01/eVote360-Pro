using eVote360Pro.Core.Application.DTOs;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IVotoService
    {
        Task<bool> RegistrarVotoAsync(VotoDTO dto);

        Task<bool> CiudadanoYaVotoAsync(int ciudadanoId, int eleccionId);

        Task<int> ObtenerCantidadVotantesAsync(int eleccionId);
    }
}