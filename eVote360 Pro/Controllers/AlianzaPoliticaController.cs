using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.AlianzaPolitica;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class AlianzaPoliticaController : Controller
    {
        private readonly IAlianzaPoliticaService _service;
        private readonly IMapper _mapper;

        public AlianzaPoliticaController(
            IAlianzaPoliticaService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var alianzas =
                await _service.GetAllAsync();

            return View(
                _mapper.Map<
                    List<AlianzaPoliticaViewModel>>
                    (alianzas));
        }

        public IActionResult Create()
        {
            return View("Save",
                new SaveAlianzaPoliticaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            SaveAlianzaPoliticaViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("Save", vm);

            await _service.AddAsync(
                _mapper.Map<AlianzaPoliticaDto>(vm));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto =
                await _service.GetByIdAsync(id);

            if (dto == null)
                return RedirectToAction(nameof(Index));

            return View("Save",
                _mapper.Map<
                    SaveAlianzaPoliticaViewModel>
                    (dto));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            SaveAlianzaPoliticaViewModel vm)
        {
            await _service.UpdateAsync(
                vm.Id,
                _mapper.Map<
                    AlianzaPoliticaDto>(vm));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Activar(int id)
        {
            await _service.ActivarAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Desactivar(int id)
        {
            await _service.DesactivarAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
