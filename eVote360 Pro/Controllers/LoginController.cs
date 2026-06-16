using eVote360Pro.Core.Application.Dtos.User;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Helpers;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.Usuario;
using eVote360Pro.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUserSession _userSession;

        public LoginController(
            IUsuarioService usuarioService,
            IUserSession userSession)
        {
            _usuarioService = usuarioService;
            _userSession = userSession;
        }

        public IActionResult Index()
        {
            if (_userSession.HasUser())
            {
                var usuarioSession = _userSession.GetUserSession();

                if (usuarioSession != null)
                {
                    return RedirigirPorRol(usuarioSession.RolUsuario);
                }
            }

            return View(new LoginViewModel
            {
                NombreUsuario = "",
                Contrasena = ""
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Contrasena = "";
                return View(vm);
            }

            var usuarioDto = await _usuarioService.LoginAsync(new LoginDto
            {
                NombreUsuario = vm.NombreUsuario,
                Contrasena = vm.Contrasena
            });

            if (usuarioDto == null)
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos, usuario inactivo o dirigente sin partido asignado.");
                vm.Contrasena = "";
                return View(vm);
            }

            HttpContext.Session.Set<UsuarioDto>("Usuario", usuarioDto);

            return RedirigirPorRol(usuarioDto.RolUsuario);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Usuario");

            return RedirectToAction("Index", "Login");
        }

        public IActionResult AccessDenied()
        {
            if (_userSession.HasUser())
                return View();

            return RedirectToAction("Index", "Login");
        }

        private IActionResult RedirigirPorRol(RolUsuario rol)
        {
            return rol switch
            {
                RolUsuario.Administrador => RedirectToAction("Index", "HomeAdministrador"),
                RolUsuario.Dirigente => RedirectToAction("Index", "HomeDirigente"),
                _ => RedirectToAction("Index", "Login")
            };
        }
    }
}