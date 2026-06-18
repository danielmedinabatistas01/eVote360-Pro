using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.AsignacionDirigente;
using Microsoft.AspNetCore.Mvc;

namespace eVote360Pro.Controllers
{
    public class AsignacionDirigenteController : Controller
    {
        private readonly IAsignacionDirigenteService _service;
        private readonly IUsuarioService _usuarioService;
        private readonly IPartidoPoliticoService _partidoService;
        private readonly IMapper _mapper;
        private readonly IUserSession _userSession;

        public AsignacionDirigenteController(
            IAsignacionDirigenteService service,
            IUsuarioService usuarioService,
            IPartidoPoliticoService partidoService,
            IMapper mapper,
            IUserSession userSession)
        {
            _service = service;
            _usuarioService = usuarioService;
            _partidoService = partidoService;
            _mapper = mapper;
            _userSession = userSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var asignaciones = await _service.GetAllAsync();

            return View(
                _mapper.Map<List<AsignacionDirigenteViewModel>>(asignaciones));
        }

        public async Task<IActionResult> Create()
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            // Se deben cargar los listados de usuarios y partidos para el dropdown en la vista
            ViewBag.UsuariosDirigentes = await _usuarioService.GetAllAsync();
            ViewBag.PartidosPoliticos = await _partidoService.GetAllAsync();

            return View("Save", new SaveAsignacionDirigenteViewModel());
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
                // si el modelo no es válido, recargar los listados para el dropdown y retornar la vista con el modelo actual
                ViewBag.UsuariosDirigentes = await _usuarioService.GetAllAsync();
                ViewBag.PartidosPoliticos = await _partidoService.GetAllAsync();
                return View("Save", vm);
            }

            var dto = _mapper.Map<AsignacionDirigenteDto>(vm);

            await _service.AddAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var dto = await _service.GetByIdAsync(id);

            if (dto == null)
                return RedirectToAction(nameof(Index));

            var vm = _mapper.Map<SaveAsignacionDirigenteViewModel>(dto);

            ViewBag.UsuariosDirigentes = await _usuarioService.GetAllAsync();
            ViewBag.PartidosPoliticos = await _partidoService.GetAllAsync();

            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            SaveAsignacionDirigenteViewModel vm)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            if (!ModelState.IsValid)
            {
                ViewBag.UsuariosDirigentes = await _usuarioService.GetAllAsync();
                ViewBag.PartidosPoliticos = await _partidoService.GetAllAsync();
                return View("Save", vm);
            }

            var dto = _mapper.Map<AsignacionDirigenteDto>(vm);

            await _service.UpdateAsync(vm.Id, dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}