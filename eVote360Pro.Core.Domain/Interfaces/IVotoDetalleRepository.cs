using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface IVotoDetalleRepository : IGenericRepository<VotoDetalle>
    {
        Task<List<VotoDetalle>> GetByEleccionIdAsync(int eleccionId);
    }
}