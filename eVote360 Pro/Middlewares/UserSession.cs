using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Helpers;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Enums;

namespace eVote360_Pro.Middlewares
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            var usuario = _httpContextAccessor.HttpContext?
                .Session.Get<UsuarioDto>("Usuario");

            return usuario != null;
        }

        public UsuarioDto? GetUserSession()
        {
            return _httpContextAccessor.HttpContext?
                .Session.Get<UsuarioDto>("Usuario");
        }

        public bool IsAdmin()
        {
            var usuario = GetUserSession();

            return usuario != null &&
                   usuario.RolUsuario == RolUsuario.Administrador;
        }

        public bool IsDirigente()
        {
            var usuario = GetUserSession();

            return usuario != null &&
                   usuario.RolUsuario == RolUsuario.Dirigente;
        }
    }
}