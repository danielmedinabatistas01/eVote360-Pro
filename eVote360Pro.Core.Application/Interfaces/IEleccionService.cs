using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.ViewModels.Eleccion;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IEleccionService : IGenericService<EleccionDTO>
    {
        Task<List<EleccionIndexViewModel>> GetAllAsync();

        Task<EleccionEditViewModel?> GetEditViewModelByIdAsync(int id);

        Task<EleccionActivarViewModel?> GetActivarViewModelAsync(int id);

        Task<EleccionFinalizarViewModel?> GetFinalizarViewModelAsync(int id);

        Task ActivarAsync(int id);

        Task FinalizarAsync(int id);

        Task<bool> ExisteEleccionActivaAsync();

        Task<EleccionDTO?> GetEleccionActivaAsync();
    }
}