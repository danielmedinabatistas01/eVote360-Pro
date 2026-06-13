using eVote360Pro.Core.Application.DTOs;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IVotoService
    {
        Task CrearVotoAsync(VotoDTO dto);

        Task<bool> CiudadanoYaVotoAsync(int ciudadanoId, int eleccionId);

        Task<int> CountCiudadanosVotaronAsync(int eleccionId);

        Task<List<VotoDTO>> GetByEleccionIdAsync(int eleccionId);
    }
}