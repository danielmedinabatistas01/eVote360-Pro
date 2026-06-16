using eVote360Pro.Core.Application.DTOs;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IUserSession
    {
        bool HasUser();
        UsuarioDto? GetUserSession();
        bool IsAdmin();
        bool IsDirigente();
    }
}