using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface IPartidoPoliticoRepository: IGenericRepository<PartidoPolitico>
    {
        Task<bool> ExisteSiglaAsync(string sigla);
        Task<List<PartidoPolitico>> ObtenerActivosAsync();
    }
}
