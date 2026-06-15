using eVote360Pro.Core.Application.ViewModels.ResultadoElectoral;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IResultadoElectoralService
    {
        Task<ResultadoElectoralIndexViewModel?> GetResultadosByEleccionIdAsync(int eleccionId);
    }
}