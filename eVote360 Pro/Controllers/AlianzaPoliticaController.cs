using AutoMapper;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.AlianzaPolitica;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class AlianzaPoliticaController : Controller
    {
        private readonly IAlianzaPoliticaService _alianzaService;
        private readonly IMapper _mapper;

        public AlianzaPoliticaController(
            IAlianzaPoliticaService alianzaService,
            IMapper mapper)
        {
            _alianzaService = alianzaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var dtoList = await _alianzaService.GetActivosAsync();

            var vm = _mapper.Map<List<AlianzaPoliticaViewModel>>(dtoList);

            return View(vm);
        }

        public IActionResult Create()
        {
            return View(new SaveAlianzaPoliticaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveAlianzaPoliticaViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                await _alianzaService.CrearSolicitudAsync(
                    vm.PartidoOrigenId,
                    vm.PartidoDestinoId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        public async Task<IActionResult> Aceptar(int id)
        {
            try
            {
                await _alianzaService.AceptarSolicitudAsync(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Rechazar(int id)
        {
            try
            {
                await _alianzaService.RechazarSolicitudAsync(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EliminarSolicitud(int id)
        {
            try
            {
                await _alianzaService.EliminarSolicitudAsync(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EliminarAlianza(int id)
        {
            try
            {
                await _alianzaService.EliminarAlianzaAsync(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}