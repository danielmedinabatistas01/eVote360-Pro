using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.AsignacionCandidato;
using eVote360Pro.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eVote360_Pro.Controllers
{
    public class AsignacionCandidatoController : Controller
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
                return RedirectToAction("Index", "Login");

            var dtoList = await _service.GetAllAsync();

            var candidatos = await _candidatoService.GetByPartidoPoliticoAsync(
                usuario.PartidoPoliticoId.Value);

            var candidatoIds = candidatos
                .Select(c => c.Id)
                .ToHashSet();

            var filteredList = dtoList
                .Where(x => candidatoIds.Contains(x.CandidatoId))
                .ToList();

            var vm = _mapper.Map<List<AsignacionCandidatoViewModel>>(filteredList);

            ViewBag.HasActiveElection = await _eleccionService.ExisteEleccionActivaAsync();

            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsDirigente())
                return RedirectToAction("AccessDenied", "Login");

            if (await _eleccionService.ExisteEleccionActivaAsync())
            {
                TempData["Error"] = "No se pueden crear asignaciones mientras exista una elección activa.";
                return RedirectToAction(nameof(Index));
            }

            var usuario = _userSession.GetUserSession();

            if (usuario == null || !usuario.PartidoPoliticoId.HasValue)
                return RedirectToAction("Index", "Login");

            await LoadDropdowns(usuario.PartidoPoliticoId.Value);

            return View(new SaveAsignacionCandidatoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaveAsignacionCandidatoViewModel vm)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsDirigente())
                return RedirectToAction("AccessDenied", "Login");

            if (await _eleccionService.ExisteEleccionActivaAsync())
            {
                TempData["Error"] = "No se pueden crear asignaciones mientras exista una elección activa.";
                return RedirectToAction(nameof(Index));
            }

            var usuario = _userSession.GetUserSession();

            if (usuario == null || !usuario.PartidoPoliticoId.HasValue)
                return RedirectToAction("Index", "Login");

            int partidoDirigenteId = usuario.PartidoPoliticoId.Value;

            if (!ModelState.IsValid)
            {
                await LoadDropdowns(partidoDirigenteId);
                return View(vm);
            }

            try
            {
                var dto = _mapper.Map<AsignacionCandidatoDto>(vm);

                await _service.AsignarCandidatoAsync(dto, partidoDirigenteId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                await LoadDropdowns(partidoDirigenteId);

                return View(vm);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsDirigente())
                return RedirectToAction("AccessDenied", "Login");

            if (await _eleccionService.ExisteEleccionActivaAsync())
            {
                TempData["Error"] = "No se pueden eliminar asignaciones mientras exista una elección activa.";
                return RedirectToAction(nameof(Index));
            }

            var usuario = _userSession.GetUserSession();

            if (usuario == null || !usuario.PartidoPoliticoId.HasValue)
                return RedirectToAction("Index", "Login");

            var dto = await _service.GetByIdAsync(id);

            if (dto == null)
                return RedirectToAction(nameof(Index));

            var vm = _mapper.Map<AsignacionCandidatoViewModel>(dto);

            return View(vm);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsDirigente())
                return RedirectToAction("AccessDenied", "Login");

            if (await _eleccionService.ExisteEleccionActivaAsync())
            {
                TempData["Error"] = "No se pueden eliminar asignaciones mientras exista una elección activa.";
                return RedirectToAction(nameof(Index));
            }

            var usuario = _userSession.GetUserSession();

            if (usuario == null || !usuario.PartidoPoliticoId.HasValue)
                return RedirectToAction("Index", "Login");

            try
            {
                await _service.EliminarAsignacionAsync(
                    id,
                    usuario.PartidoPoliticoId.Value);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }

        private async Task LoadDropdowns(int partidoDirigenteId)
        {
            var candidatos = await _candidatoService.GetByPartidoPoliticoAsync(partidoDirigenteId);
            var puestos = await _puestoService.GetAllAsync();
            var elecciones = await _eleccionService.GetAllAsync();

            ViewBag.Candidatos = candidatos
                .Where(x => x.Estado)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Nombre} {x.Apellido}"
                })
                .ToList();

            ViewBag.Puestos = puestos
                .Where(x => x.EsActivo)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nombre
                })
                .ToList();

            ViewBag.Elecciones = elecciones
                .Where(x => x.EstadoEleccion == EstadoEleccion.Pendiente)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Nombre
                })
                .ToList();
        }
    }
}