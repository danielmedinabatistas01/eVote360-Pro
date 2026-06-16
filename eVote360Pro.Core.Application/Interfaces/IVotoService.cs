using eVote360Pro.Core.Application.Dtos;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IVotoService : IGenericService<VotoDto>
    {
        Task<bool> CiudadanoYaVotoAsync(int ciudadanoId, int eleccionId);

        Task<bool> PuedeVotarAsync(int ciudadanoId, int eleccionId);

        Task RegistrarVotoAsync(VotoDto dto);

        Task CrearVotoAsync(VotoDto dto);

        Task<int> CountCiudadanosVotaronAsync(int eleccionId);

        Task<List<VotoDto>> GetByEleccionIdAsync(int eleccionId);
    }
}