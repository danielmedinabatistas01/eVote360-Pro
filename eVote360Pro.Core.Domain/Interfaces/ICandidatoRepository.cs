using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface ICandidatoRepository : IGenericRepository<Candidato>
    {
        Task<List<Candidato>> GetActivosAsync();


    }
}
