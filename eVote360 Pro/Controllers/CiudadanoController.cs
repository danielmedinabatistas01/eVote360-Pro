using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.Ciudadano;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class CiudadanoController : Controller
    {
        private readonly ICiudadanoService _service;
        private readonly IMapper _mapper;

        public CiudadanoController(ICiudadanoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var ciudadanos = await _service.GetAllAsync();
            return View(_mapper.Map<List<CiudadanoViewModel>>(ciudadanos));
        }

        public IActionResult Create()
        {
            return View("Save", new CiudadanoViewModel()
            {
                NumeroIdentificacion = string.Empty,
                Nombre = string.Empty,
                Apellido = string.Empty,
                CorreoElectronico = string.Empty,
                EsActivo = true
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CiudadanoViewModel vm)
        {
            if (!ModelState.IsValid) return View("Save", vm);

            try
            {
                var dto = _mapper.Map<CiudadanoDto>(vm);
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

            var vm = _mapper.Map<CiudadanoViewModel>(dto);
            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CiudadanoViewModel vm)
        {
            if (!ModelState.IsValid) return View("Save", vm);

            try
            {
                var dto = _mapper.Map<CiudadanoDto>(vm);
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
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}