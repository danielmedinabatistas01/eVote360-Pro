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
        private readonly IEleccionService _eleccionService;
        private readonly IUserSession _userSession;

        public UsuarioController(
            IUsuarioService usuarioService,
            IEleccionService eleccionService,
            IUserSession userSession)
        {
            _usuarioService = usuarioService;
            _eleccionService = eleccionService;
            _userSession = userSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            ViewBag.HasActiveElection = await _eleccionService.ExisteEleccionActivaAsync();

            var usuarios = await _usuarioService.GetAllAsync();
            return View(usuarios);
        }

        public async Task<IActionResult> Create()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            if (await _eleccionService.ExisteEleccionActivaAsync())
            {
                TempData["ErrorMessage"] = "No se pueden crear usuarios mientras exista una elección activa.";
                return RedirectToAction(nameof(Index));
            }

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

            if (await _eleccionService.ExisteEleccionActivaAsync())
            {
                TempData["ErrorMessage"] = "No se pueden crear usuarios mientras exista una elección activa.";
                return RedirectToAction(nameof(Index));
            }

            vm.Nombre = vm.Nombre?.Trim() ?? string.Empty;
            vm.Apellido = vm.Apellido?.Trim() ?? string.Empty;
            vm.NombreUsuario = vm.NombreUsuario?.Trim() ?? string.Empty;
            vm.CorreoElectronico = vm.CorreoElectronico?.Trim() ?? string.Empty;
            vm.Contrasena = vm.Contrasena?.Trim() ?? string.Empty;
            vm.ConfirmarContrasena = vm.ConfirmarContrasena?.Trim() ?? string.Empty;

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

                TempData["SuccessMessage"] = "Usuario creado correctamente.";
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

            if (await _eleccionService.ExisteEleccionActivaAsync())
            {
                TempData["ErrorMessage"] = "No se pueden editar usuarios mientras exista una elección activa.";
                return RedirectToAction(nameof(Index));
            }

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

            if (await _eleccionService.ExisteEleccionActivaAsync())
            {
                TempData["ErrorMessage"] = "No se pueden editar usuarios mientras exista una elección activa.";
                return RedirectToAction(nameof(Index));
            }

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
                    Contrasena = vm.Contrasena,
                    ConfirmarContrasena = vm.ConfirmarContrasena,
                    RolUsuario = vm.RolUsuario,
                    Estado = vm.Estado,
                    PartidoPoliticoId = vm.RolUsuario == RolUsuario.Dirigente
                        ? vm.PartidoPoliticoId
                        : null
                };

                await _usuarioService.UpdateAsync(dto);

                TempData["SuccessMessage"] = "Usuario actualizado correctamente.";
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

            if (await _eleccionService.ExisteEleccionActivaAsync())
            {
                TempData["ErrorMessage"] = "No se pueden activar usuarios mientras exista una elección activa.";
                return RedirectToAction(nameof(Index));
            }

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

            if (await _eleccionService.ExisteEleccionActivaAsync())
            {
                TempData["ErrorMessage"] = "No se pueden activar usuarios mientras exista una elección activa.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _usuarioService.ActivarAsync(id);
                TempData["SuccessMessage"] = "Usuario activado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Desactivar(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            if (await _eleccionService.ExisteEleccionActivaAsync())
            {
                TempData["ErrorMessage"] = "No se pueden desactivar usuarios mientras exista una elección activa.";
                return RedirectToAction(nameof(Index));
            }

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

            if (await _eleccionService.ExisteEleccionActivaAsync())
            {
                TempData["ErrorMessage"] = "No se pueden desactivar usuarios mientras exista una elección activa.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _usuarioService.DesactivarAsync(id);
                TempData["SuccessMessage"] = "Usuario desactivado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}