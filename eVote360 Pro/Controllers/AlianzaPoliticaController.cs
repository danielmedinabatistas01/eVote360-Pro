using AutoMapper;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.AlianzaPolitica;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class AlianzaPoliticaController : Controller
    {
        private readonly IAlianzaPoliticaService _alianzaService;
        private readonly IPartidoPoliticoService _partidoService;
        private readonly IMapper _mapper;
        private readonly IUserSession _userSession;

        public AlianzaPoliticaController(
            IAlianzaPoliticaService alianzaService,
            IPartidoPoliticoService partidoService,
            IMapper mapper,
            IUserSession userSession)
        {
            _alianzaService = alianzaService;
            _partidoService = partidoService;
            _mapper = mapper;
            _userSession = userSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsDirigente())
                return RedirectToAction("AccessDenied", "Login");

            var dtoList = await _alianzaService.GetActivosAsync();

            var vm = _mapper.Map<List<AlianzaPoliticaViewModel>>(dtoList);

            return View(vm);
        }

        public async Task<IActionResult> CrearSolicitud()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsDirigente())
                return RedirectToAction("AccessDenied", "Login");

            var usuario = _userSession.GetUserSession();
            if (usuario == null || !usuario.PartidoPoliticoId.HasValue)
            {
                return RedirectToAction("Index", "Login");
            }

            var partidos = await _partidoService.GetAllAsync();
            ViewBag.PartidosDestino = partidos
                .Where(x => x.EsActivo && x.Id != usuario.PartidoPoliticoId.Value)
                .ToList();

            return View(new SaveAlianzaPoliticaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CrearSolicitud(SaveAlianzaPoliticaViewModel vm)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsDirigente())
                return RedirectToAction("AccessDenied", "Login");

            var usuario = _userSession.GetUserSession();
            if (usuario == null || !usuario.PartidoPoliticoId.HasValue)
            {
                return RedirectToAction("Index", "Login");
            }

            try
            {
                vm.PartidoOrigenId = usuario.PartidoPoliticoId.Value;

                if (!ModelState.IsValid)
                {
                    var partidos = await _partidoService.GetAllAsync();
                    ViewBag.PartidosDestino = partidos
                        .Where(x => x.EsActivo && x.Id != usuario.PartidoPoliticoId.Value)
                        .ToList();
                    return View(vm);
                }

                await _alianzaService.CrearSolicitudAsync(
                        vm.PartidoOrigenId,
                        vm.PartidoDestinoId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                var partidos = await _partidoService.GetAllAsync();
                ViewBag.PartidosDestino = partidos
                    .Where(x => x.EsActivo && x.Id != usuario.PartidoPoliticoId.Value)
                    .ToList();

                return View(vm);
            }
        }

        public async Task<IActionResult> Aceptar(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsDirigente())
                return RedirectToAction("AccessDenied", "Login");

            try
            {
                await _alianzaService.AceptarSolicitudAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Rechazar(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsDirigente())
                return RedirectToAction("AccessDenied", "Login");

            try
            {
                await _alianzaService.RechazarSolicitudAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> EliminarSolicitud(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsDirigente())
                return RedirectToAction("AccessDenied", "Login");

            try
            {
                await _alianzaService.EliminarSolicitudAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> EliminarAlianza(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsDirigente())
                return RedirectToAction("AccessDenied", "Login");

            try
            {
                await _alianzaService.EliminarAlianzaAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }
    }
}