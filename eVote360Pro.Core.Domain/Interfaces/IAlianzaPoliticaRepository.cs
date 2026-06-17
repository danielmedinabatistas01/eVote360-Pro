using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public interface IAlianzaPoliticaRepository: IGenericRepository<AlianzaPolitica>
    {
        Task<List<AlianzaPolitica>> GetActivosAsync();
    }
}
