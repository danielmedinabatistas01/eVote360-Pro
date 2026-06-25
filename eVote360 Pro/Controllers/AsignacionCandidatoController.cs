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
        private readonly IAsignacionCandidatoService _service;
        private readonly ICandidatoService _candidatoService;
        private readonly IPuestoElectivoService _puestoService;
        private readonly IEleccionService _eleccionService;
        private readonly IUserSession _userSession;
        private readonly IMapper _mapper;

        public AsignacionCandidatoController(
            IAsignacionCandidatoService service,
            ICandidatoService candidatoService,
            IPuestoElectivoService puestoService,
            IEleccionService eleccionService,
            IUserSession userSession,
            IMapper mapper)
        {
            _service = service;
            _candidatoService = candidatoService;
            _puestoService = puestoService;
            _eleccionService = eleccionService;
            _userSession = userSession;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
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

            var dtoList = await _service.GetAllAsync();
            var candidatos = await _candidatoService.GetByPartidoPoliticoAsync(usuario.PartidoPoliticoId.Value);
            var candidatoIds = candidatos.Select(c => c.Id).ToHashSet();

            var filteredList = dtoList.Where(x => candidatoIds.Contains(x.CandidatoId)).ToList();
            var vm = _mapper.Map<List<AsignacionCandidatoViewModel>>(filteredList);

            return View(vm);
        }

        public async Task<IActionResult> Create()
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

            var candidatos = await _candidatoService.GetByPartidoPoliticoAsync(usuario.PartidoPoliticoId.Value);
            var puestos = await _puestoService.GetAllAsync();
            var elecciones = await _eleccionService.GetAllAsync();

            ViewBag.Candidatos = candidatos.Where(x => x.Estado).ToList();
            ViewBag.Puestos = puestos.Where(x => x.EsActivo).ToList();
            ViewBag.Elecciones = elecciones.Where(x => x.EstadoEleccion == eVote360Pro.Core.Domain.Enums.EstadoEleccion.Pendiente).ToList();

            return View(new SaveAsignacionCandidatoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAsignacionCandidatoViewModel vm)
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

            int partidoDirigenteId = usuario.PartidoPoliticoId.Value;

            try
            {
                if (!ModelState.IsValid)
                {
                    var candidatos = await _candidatoService.GetByPartidoPoliticoAsync(partidoDirigenteId);
                    var puestos = await _puestoService.GetAllAsync();
                    var elecciones = await _eleccionService.GetAllAsync();

                    ViewBag.Candidatos = candidatos.Where(x => x.Estado).ToList();
                    ViewBag.Puestos = puestos.Where(x => x.EsActivo).ToList();
                    ViewBag.Elecciones = elecciones.Where(x => x.EstadoEleccion == eVote360Pro.Core.Domain.Enums.EstadoEleccion.Pendiente).ToList();

                    return View(vm);
                }

                var dto = _mapper.Map<AsignacionCandidatoDto>(vm);

                await _service.AsignarCandidatoAsync(
                        dto,
                        partidoDirigenteId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    ex.Message);

                var candidatos = await _candidatoService.GetByPartidoPoliticoAsync(partidoDirigenteId);
                var puestos = await _puestoService.GetAllAsync();
                var elecciones = await _eleccionService.GetAllAsync();

                ViewBag.Candidatos = candidatos.Where(x => x.Estado).ToList();
                ViewBag.Puestos = puestos.Where(x => x.EsActivo).ToList();
                ViewBag.Elecciones = elecciones.Where(x => x.EstadoEleccion == eVote360Pro.Core.Domain.Enums.EstadoEleccion.Pendiente).ToList();

                return View(vm);
            }
        }

        public async Task<IActionResult> Delete(int id)
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
                int partidoDirigenteId = usuario.PartidoPoliticoId.Value;

                await _service.EliminarAsignacionAsync(
                        id,
                        partidoDirigenteId);

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