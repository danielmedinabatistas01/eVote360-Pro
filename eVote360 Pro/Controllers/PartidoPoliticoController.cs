using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.PartidoPolitico;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class PartidoPoliticoController : Controller
    {
        private readonly IPartidoPoliticoService _service;
        private readonly IMapper _mapper;

        public PartidoPoliticoController(IPartidoPoliticoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var partidos = await _service.GetAllAsync();
            return View(_mapper.Map<List<PartidoPoliticoViewModel>>(partidos));
        }

        public IActionResult Create()
        {
            return View("Save", new PartidoPoliticoViewModel()
            {
                Nombre = string.Empty,
                Siglas = string.Empty,
                LogoUrl = string.Empty,
                EsActivo = true
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartidoPoliticoViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("Save", vm);

            try
            {
                var dto = _mapper.Map<PartidoPoliticoDto>(vm);
                await _service.AddAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Save", vm);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return RedirectToAction(nameof(Index));

            var vm = _mapper.Map<PartidoPoliticoViewModel>(dto);
            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PartidoPoliticoViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("Save", vm);

            try
            {
                var dto = _mapper.Map<PartidoPoliticoDto>(vm);
                await _service.UpdateAsync(vm.Id, dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Save", vm);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Guarda el mensaje de validación de negocio para mostrarlo en el Index
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}