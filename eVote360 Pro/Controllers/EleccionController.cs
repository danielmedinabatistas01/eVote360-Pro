using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.Eleccion;
using eVote360Pro.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class EleccionController : Controller
    {
        private readonly IEleccionService _eleccionService;
        private readonly IUserSession _userSession;

        public EleccionController(
            IEleccionService eleccionService,
            IUserSession userSession)
        {
            _eleccionService = eleccionService;
            _userSession = userSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var elecciones = await _eleccionService.GetAllAsync();

            return View(elecciones);
        }

        public IActionResult Create()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            return View(new EleccionCreateViewModel
            {
                FechaRealizacion = DateTime.Today
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EleccionCreateViewModel vm)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            if (!ModelState.IsValid)
                return View(vm);

            var dto = new EleccionDTO
            {
                Nombre = vm.Nombre,
                FechaRealizacion = vm.FechaRealizacion,
                EstadoEleccion = EstadoEleccion.Configurada
            };

            await _eleccionService.AddAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var vm = await _eleccionService.GetEditViewModelByIdAsync(id);

            if (vm == null)
                return RedirectToAction(nameof(Index));

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EleccionEditViewModel vm)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            if (!ModelState.IsValid)
                return View(vm);

            var dto = new EleccionDTO
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                FechaRealizacion = vm.FechaRealizacion,
                EstadoEleccion = vm.EstadoEleccion
            };

            await _eleccionService.UpdateAsync(vm.Id, dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activar(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var vm = await _eleccionService.GetActivarViewModelAsync(id);

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

            await _eleccionService.ActivarAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Finalizar(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var vm = await _eleccionService.GetFinalizarViewModelAsync(id);

            if (vm == null)
                return RedirectToAction(nameof(Index));

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarConfirmado(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            await _eleccionService.FinalizarAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}