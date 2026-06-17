using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public interface IAlianzaPoliticaRepository
        : IGenericRepository<AlianzaPolitica>
    {
        Task<List<AlianzaPolitica>>
            GetActivosAsync();

        Task<bool>
            ExisteAlianzaAsync(
                int partidoOrigenId,
                int partidoDestinoId);

        Task<bool>
            ExisteSolicitudPendienteAsync(
                int partidoOrigenId,
                int partidoDestinoId);

        Task<List<AlianzaPolitica>>
            ObtenerPendientesAsync(
                int partidoDestinoId);
    }
}