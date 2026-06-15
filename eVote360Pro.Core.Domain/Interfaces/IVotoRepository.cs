using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface IVotoRepository : IGenericRepository<Voto>
    {
        Task<bool> CiudadanoYaVotoAsync(int ciudadanoId, int eleccionId);

        Task<int> CountCiudadanoYaVotoAsync(int eleccionId);
    }
}