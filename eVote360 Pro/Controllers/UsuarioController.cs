using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.Usuario;
using eVote360Pro.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUserSession _userSession;

        public UsuarioController(
            IUsuarioService usuarioService,
            IUserSession userSession)
        {
            _usuarioService = usuarioService;
            _userSession = userSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var usuarios = await _usuarioService.GetAllAsync();

            return View(usuarios);
        }

        public IActionResult Create()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            return View(new UsuarioCreateViewModel
            {
                Estado = true
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioCreateViewModel vm)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                var dto = new UsuarioDto
                {
                    Nombre = vm.Nombre,
                    Apellido = vm.Apellido,
                    NombreUsuario = vm.NombreUsuario,
                    CorreoElectronico = vm.CorreoElectronico,
                    Contrasena = vm.Contrasena,
                    ConfirmarContrasena = vm.ConfirmarContrasena,
                    RolUsuario = vm.RolUsuario,
                    Estado = true,
                    PartidoPoliticoId = vm.RolUsuario == RolUsuario.Dirigente
                        ? vm.PartidoPoliticoId
                        : null
                };

                await _usuarioService.CreateAsync(dto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var vm = await _usuarioService.GetByIdAsync(id);

            if (vm == null)
                return RedirectToAction(nameof(Index));

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuarioEditViewModel vm)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                var dto = new UsuarioDto
                {
                    Id = vm.Id,
                    Nombre = vm.Nombre,
                    Apellido = vm.Apellido,
                    NombreUsuario = vm.NombreUsuario,
                    CorreoElectronico = vm.CorreoElectronico,
                    Contrasena = vm.Contrasena ?? string.Empty,
                    ConfirmarContrasena = vm.ConfirmarContrasena ?? string.Empty,
                    RolUsuario = vm.RolUsuario,
                    Estado = vm.Estado,
                    PartidoPoliticoId = vm.RolUsuario == RolUsuario.Dirigente
                        ? vm.PartidoPoliticoId
                        : null
                };

                await _usuarioService.UpdateAsync(dto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        public async Task<IActionResult> Activar(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var vm = await _usuarioService.GetActivarViewModelAsync(id);

            if (vm == null)
                return RedirectToAction(nameof(Index));

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivarConfirmado(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            await _usuarioService.ActivarAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Desactivar(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var vm = await _usuarioService.GetDesactivarViewModelAsync(id);

            if (vm == null)
                return RedirectToAction(nameof(Index));

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DesactivarConfirmado(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            await _usuarioService.DesactivarAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}