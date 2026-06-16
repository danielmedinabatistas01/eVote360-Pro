using eVote360Pro.Core.Application.DTOs;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IEleccionPuestoElectivoService
        : IGenericService<EleccionPuestoElectivoDTO>
    {
        Task AsignarAsync(EleccionPuestoElectivoDTO dto);
        Task<List<EleccionPuestoElectivoDTO>> GetByEleccionIdAsync(int eleccionId);
    }
}