using eVote360Pro.Core.Application.Dtos;


namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IVotoService
    {

        Task<bool> CiudadanoYaVotoAsync(int ciudadanoId, int eleccionId);

        Task RegistrarVotoAsync(
            VotoDto dto);
        Task<int> CountCiudadanosVotaronAsync(int eleccionId);

        Task<bool> PuedeVotarAsync(
            int ciudadanoId,
            int eleccionId);
        Task<List<VotoDto>> GetByEleccionIdAsync(int eleccionId);
    }
}
