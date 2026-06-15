using eVote360Pro.Core.Application.DTOs;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IResultadoElectoralService
    {
        Task<List<ResultadoElectoralDTO>> GetResultadosByEleccionIdAsync(int eleccionId);
    }
}