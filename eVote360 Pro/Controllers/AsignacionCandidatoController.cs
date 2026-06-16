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

        private readonly IMapper _mapper;

        public AsignacionCandidatoController(
            IAsignacionCandidatoService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var asignaciones =
                await _service.GetAllAsync();

            return View(
                _mapper.Map<
                    List<AsignacionCandidatoViewModel>>
                    (asignaciones));
        }

        public IActionResult Create()
        {
            return View("Save",
                new SaveAsignacionCandidatoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            SaveAsignacionCandidatoViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("Save", vm);

            await _service.AsignarCandidatoAsync(
                _mapper.Map<
                    AsignacionCandidatoDto>(vm));

            return RedirectToAction(nameof(Index));
        }
    }
}
