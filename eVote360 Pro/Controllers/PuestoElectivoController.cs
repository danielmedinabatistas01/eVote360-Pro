using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.PuestoElectivo;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class PuestoElectivoController : Controller
    {
        private readonly IPuestoElectivoService _service;
        private readonly IMapper _mapper;

        public PuestoElectivoController(IPuestoElectivoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var puestos = await _service.GetAllAsync();
            return View(_mapper.Map<List<PuestoElectivoViewModel>>(puestos));
        }

        public IActionResult Create()
        {
            return View("Save", new PuestoElectivoViewModel()
            {
                Nombre = string.Empty,
                Descripcion = string.Empty,
                EsActivo = true
                
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(PuestoElectivoViewModel vm)
        {
            if (!ModelState.IsValid) return View("Save", vm);

            try
            {
                var dto = _mapper.Map<PuestoElectivoDto>(vm);
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

            var vm = _mapper.Map<PuestoElectivoViewModel>(dto);
            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PuestoElectivoViewModel vm)
        {
            if (!ModelState.IsValid) return View("Save", vm);

            try
            {
                var dto = _mapper.Map<PuestoElectivoDto>(vm);
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