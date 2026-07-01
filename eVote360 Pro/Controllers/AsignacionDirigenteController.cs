using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.AsignacionDirigente;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class AsignacionDirigenteController : Controller
    {
        private readonly IAsignacionDirigenteService _service;
        private readonly IUserSession _userSession;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;
        private readonly IPartidoPoliticoService _partidoService;

        public AsignacionDirigenteController(
            IAsignacionDirigenteService service,
            IUserSession userSession,
            IMapper mapper,
             IUsuarioService usuarioService,
    IPartidoPoliticoService partidoService)
        {
            _service = service;
            _userSession = userSession;
            _mapper = mapper;
            _usuarioService = usuarioService;
            _partidoService = partidoService;
        }
        private async Task LoadData()
        {
            ViewBag.UsuariosDirigentes =
                await _usuarioService.GetAllAsync();

            ViewBag.PartidosPoliticos =
                await _partidoService.GetAllAsync();
        }

        public async Task<IActionResult> Index()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var asignaciones =
                await _service.GetAllAsync();

            var vm =
                _mapper.Map<List<AsignacionDirigenteViewModel>>
                (asignaciones);

            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            await LoadData();

            return View("Save",
                new SaveAsignacionDirigenteViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            SaveAsignacionDirigenteViewModel vm)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            if (!ModelState.IsValid)
            {
                await LoadData();
                return View("Save", vm);
            }

            try
            {
                var dto =
                    _mapper.Map<AsignacionDirigenteDto>(vm);

                await _service.AddAsync(dto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                return View("Save", vm);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var asignacion =
                await _service.GetByIdAsync(id);

            if (asignacion == null)
                return RedirectToAction(nameof(Index));

            var vm =
                _mapper.Map<SaveAsignacionDirigenteViewModel>
                (asignacion);

            await LoadData();

            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            int id,
            SaveAsignacionDirigenteViewModel vm)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            if (!ModelState.IsValid)
            {
                await LoadData();
                return View("Save", vm);
            }

            try
            {
                var dto =
                    _mapper.Map<AsignacionDirigenteDto>(vm);

                await _service.UpdateAsync(id, dto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                await LoadData();
                return View("Save", vm);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var asignacion =
                await _service.GetByIdAsync(id);

            if (asignacion == null)
                return RedirectToAction(nameof(Index));

            var vm =
                _mapper.Map<AsignacionDirigenteViewModel>
                (asignacion);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            try
            {
                await _service.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] =
                    ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }
    }
}