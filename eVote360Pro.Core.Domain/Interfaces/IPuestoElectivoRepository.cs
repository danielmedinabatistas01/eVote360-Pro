using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface IPuestoElectivoRepository: IGenericRepository<PuestoElectivo>
    {
        Task<bool> ExisteNombreAsync(string nombre);

        Task<List<PuestoElectivo>> ObtenerActivosAsync();
    }
}
