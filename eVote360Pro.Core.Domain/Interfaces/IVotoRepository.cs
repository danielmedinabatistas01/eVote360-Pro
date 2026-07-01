using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface IVotoRepository : IGenericRepository<Voto>
    {
        Task<int> CountCiudadanosVotaronAsync(int eleccionId);

        Task<List<Voto>> GetByEleccionIdAsync(int eleccionId);
    }
}