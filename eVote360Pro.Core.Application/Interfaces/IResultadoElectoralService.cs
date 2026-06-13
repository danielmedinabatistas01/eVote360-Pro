using eVote360Pro.Core.Application.ViewModels.ResultadoElectoral;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IResultadoElectoralService
    {
        Task<List<ResultadoElectoralViewModel>> GetResultadosPorEleccionAsync(int eleccionId);
    }
}