using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface IEleccionPuestoElectivoRepository : IGenericRepository<EleccionPuestoElectivo>
    {
        Task<List<EleccionPuestoElectivo>> GetByEleccionIdAsync(int eleccionId);
    }
}