using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Helpers;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace eVote360Pro.Core.Application.Services
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
            return GetUserSession() != null;
        }

        public UsuarioDto? GetUserSession()
        {
            var session = _httpContextAccessor.HttpContext?.Session;

            if (session == null)
                return null;

            return SessionHelper.Get<UsuarioDto>(session, "Usuario");
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