using eVote360Pro.Core.Application.DTOs;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IVotoDetalleService
    {
        Task<List<VotoDetalleDTO>> GetByEleccionIdAsync(int eleccionId);

        Task CreateAsync(VotoDetalleDTO dto);
    }
}