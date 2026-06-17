using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface IEleccionRepository : IGenericRepository<Eleccion>
    {
        Task<bool> ExisteEleccionActivaAsync();

        Task<Eleccion?> GetEleccionActivaAsync();

        Task<Eleccion?> GetByIdWithPuestosAsync(int id);

        Task<List<Eleccion>> GetAllOrdenadasAsync();
    }
}