using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.ViewModels.Eleccion;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IEleccionService
    {
        Task<List<EleccionIndexViewModel>> GetAllAsync();

        Task<EleccionEditViewModel?> GetByIdAsync(int id);

        Task<EleccionActivarViewModel?> GetActivarViewModelAsync(int id);

        Task<EleccionFinalizarViewModel?> GetFinalizarViewModelAsync(int id);

        Task CreateAsync(EleccionDTO dto);

        Task UpdateAsync(EleccionDTO dto);

        Task ActivarAsync(int id);

        Task FinalizarAsync(int id);
    }
}