using AutoMapper;
using eVote360Pro.Core.Application.Dtos.User;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.Usuario;
using eVote360Pro.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public LoginController(
            IUsuarioService usuarioService,
            IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
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

            var loginCorrecto = await _usuarioService.LoginAsync(new LoginDto
            {
                NombreUsuario = vm.NombreUsuario,
                Contrasena = vm.Contrasena
            });

            if (!loginCorrecto)
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos, usuario inactivo o dirigente sin partido asignado.");
                vm.Contrasena = "";
                return View(vm);
            }

            return RedirectToAction("Index", "HomeAdministrador");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
