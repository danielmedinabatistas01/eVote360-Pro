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

        public EleccionController(IEleccionService eleccionService)
        {
            _eleccionService = eleccionService;
        }

        public async Task<IActionResult> Index()
        {
            var elecciones = await _eleccionService.GetAllAsync();
            return View(elecciones);
        }

        public IActionResult Create()
        {
            return View(new EleccionCreateViewModel
            {
                FechaRealizacion = DateTime.Today
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EleccionCreateViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var dto = new EleccionDTO
            {
                Nombre = vm.Nombre,
                FechaRealizacion = vm.FechaRealizacion,
                EstadoEleccion = EstadoEleccion.Configurada
            };

            await _eleccionService.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _eleccionService.GetByIdAsync(id);

            if (vm == null)
                return RedirectToAction(nameof(Index));

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EleccionEditViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var dto = new EleccionDTO
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                FechaRealizacion = vm.FechaRealizacion,
                EstadoEleccion = vm.EstadoEleccion
            };

            await _eleccionService.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activar(int id)
        {
            var vm = await _eleccionService.GetActivarViewModelAsync(id);

            if (vm == null)
                return RedirectToAction(nameof(Index));

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivarConfirmado(int id)
        {
            await _eleccionService.ActivarAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Finalizar(int id)
        {
            var vm = await _eleccionService.GetFinalizarViewModelAsync(id);

            if (vm == null)
                return RedirectToAction(nameof(Index));

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarConfirmado(int id)
        {
            await _eleccionService.FinalizarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}