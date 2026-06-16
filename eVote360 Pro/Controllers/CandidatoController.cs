using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.Candidato;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class CandidatoController : Controller
    {
        private readonly ICandidatoService _service;
        private readonly IMapper _mapper;

        public CandidatoController(
            ICandidatoService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var candidatos = await _service.GetAllAsync();

            return View(
                _mapper.Map<List<CandidatoViewModel>>
                (candidatos));
        }

        public IActionResult Create()
        {
            return View("Save",
                new SaveCandidatoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            SaveCandidatoViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("Save", vm);

            await _service.AddAsync(
                _mapper.Map<CandidatoDto>(vm));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _service.GetByIdAsync(id);

            if (dto == null)
                return RedirectToAction(nameof(Index));

            return View("Save",
                _mapper.Map<SaveCandidatoViewModel>(dto));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            SaveCandidatoViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("Save", vm);

            await _service.UpdateAsync(
                vm.Id,
                _mapper.Map<CandidatoDto>(vm));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activar(int id)
        {
            await _service.ActivarCandidatoAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Desactivar(int id)
        {
            await _service.DesactivarCandidatoAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
