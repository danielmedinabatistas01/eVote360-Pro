using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.AsignacionCandidato;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class AsignacionCandidatoController
        : Controller
    {
        private readonly IAsignacionCandidatoService
            _service;

        private readonly IMapper
            _mapper;

        public AsignacionCandidatoController(
            IAsignacionCandidatoService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View(
                new SaveAsignacionCandidatoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult>
            Create(
                SaveAsignacionCandidatoViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vm);
                }

                var dto =
                    _mapper.Map<
                        AsignacionCandidatoDto>(
                            vm);

                // Obtendrás este valor
                // del usuario autenticado
                int partidoDirigenteId = 0;

                await _service
                    .AsignarCandidatoAsync(
                        dto,
                        partidoDirigenteId);

                return RedirectToAction(
                    nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    ex.Message);

                return View(vm);
            }
        }

        public async Task<IActionResult>
            Delete(
                int id)
        {
            try
            {
                int partidoDirigenteId = 0;

                await _service
                    .EliminarAsignacionAsync(
                        id,
                        partidoDirigenteId);

                return RedirectToAction(
                    nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    ex.Message;

                return RedirectToAction(
                    nameof(Index));
            }
        }
    }
}