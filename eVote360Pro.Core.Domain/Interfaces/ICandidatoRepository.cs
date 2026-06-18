using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface ICandidatoRepository : IGenericRepository<Candidato>
    {
        Task<List<Candidato>> GetActivosAsync();

        Task<bool> ExisteEleccionActivaAsync();

        Task<bool> HaParticipadoEnEleccionAsync(
            int candidatoId);

        Task<bool> TieneAsignacionVigenteAsync(
            int candidatoId);

        Task<List<Candidato>>
     GetByPartidoPoliticoAsync(
         int partidoPoliticoId);

    }
}
