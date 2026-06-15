using eVote360Pro.Core.Application.Dtos.User;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.ViewModels.Usuario;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioIndexViewModel>> GetAllAsync();

        Task<UsuarioEditViewModel?> GetByIdAsync(int id);

        Task<UsuarioActivarViewModel?> GetActivarViewModelAsync(int id);

        Task<UsuarioDesactivarViewModel?> GetDesactivarViewModelAsync(int id);

        Task CreateAsync(UsuarioDto dto);

        Task UpdateAsync(UsuarioDto dto);

        Task ActivarAsync(int id);

        Task DesactivarAsync(int id);

        Task<bool> LoginAsync(LoginDto dto);
    }
}