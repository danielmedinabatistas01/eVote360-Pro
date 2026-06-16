using eVote360Pro.Core.Application.ViewModels.Usuario;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IUserSession
    {
        bool HasUser();

        UsuarioViewModel? GetUserSession();

        bool IsAdmin();

        bool IsDirigente();

        bool IsCiudadano();
    }
}