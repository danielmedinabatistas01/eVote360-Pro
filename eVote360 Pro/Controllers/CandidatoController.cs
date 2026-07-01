    using AutoMapper;
    using eVote360_Pro.Helpers;
    using eVote360Pro.Core.Application.Dtos;
    using eVote360Pro.Core.Application.Interfaces;
    using eVote360Pro.Core.Application.ViewModels.Candidato;
    using Microsoft.AspNetCore.Mvc;

    namespace eVote360_Pro.Controllers
    {
        public class CandidatoController
            : Controller
        {
            private readonly ICandidatoService _service;
            private readonly IMapper _mapper;
            private readonly IUserSession _userSession;

            public CandidatoController(
                ICandidatoService service,
                IMapper mapper,
                IUserSession userSession)
            {
                _service = service;
                _mapper = mapper;
                _userSession = userSession;
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

                var candidatos = await _service.GetByPartidoPoliticoAsync(
                        usuario.PartidoPoliticoId.Value);

                var vm = _mapper.Map<List<CandidatoViewModel>>(candidatos);

                return View(vm);
            }

            public IActionResult Create()
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

                return View(new SaveCandidatoViewModel());
            }

            [HttpPost]
            public async Task<IActionResult> Create(SaveCandidatoViewModel vm)
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

                var dto = _mapper.Map<CandidatoDto>(vm);
                dto.PartidoPoliticoId = usuario.PartidoPoliticoId.Value;
                dto.Estado = true;

                try
                {
                    if (!ModelState.IsValid)
                    {
                        return View(vm);
                    }

                    if (vm.Foto == null)
                    {
                        ModelState.AddModelError(
                            "",
                            "La foto del candidato es requerida.");

                        return View(vm);
                    }

                    string extension = Path.GetExtension(vm.Foto.FileName).ToLower();
                    string[] permitidas = { ".jpg", ".jpeg", ".png" };

                    if (!permitidas.Contains(extension))
                    {
                        ModelState.AddModelError(
                            "",
                            "La foto del candidato debe ser una imagen válida.");

                        return View(vm);
                    }

                    string? fotoUrl = FileManager.Upload(
                            vm.Foto,
                            0,
                            "candidatos");

                    dto.FotoUrl = fotoUrl;

                    await _service.AddAsync(dto);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    var error = ex.InnerException?.Message ?? ex.Message;

                    ModelState.AddModelError("", error);

                    return View(vm);
                }
            }

            public async Task<IActionResult> Edit(int id)
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

                var dto = await _service.GetByIdAsync(id);

                if (dto == null || dto.PartidoPoliticoId != usuario.PartidoPoliticoId.Value)
                {
                    return RedirectToAction(nameof(Index));
                }

                var vm = _mapper.Map<SaveCandidatoViewModel>(dto);

                return View(vm);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(SaveCandidatoViewModel vm)
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
                    var existingDto = await _service.GetByIdAsync(vm.Id);
                    if (existingDto == null || existingDto.PartidoPoliticoId != usuario.PartidoPoliticoId.Value)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    if (!ModelState.IsValid)
                    {
                        return View(vm);
                    }

                    string? fotoUrl = FileManager.Upload(
                        vm.Foto,
                        vm.Id,
                        "candidatos",
                        true,
                        vm.FotoUrl);

                    var dto = _mapper.Map<CandidatoDto>(vm);
                    dto.FotoUrl = fotoUrl;
                    dto.PartidoPoliticoId = usuario.PartidoPoliticoId.Value;

                    await _service.UpdateAsync(vm.Id, dto);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);

                    return View(vm);
                }
            }



            public async Task<IActionResult> Activar(int id)
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
                    var existingDto = await _service.GetByIdAsync(id);
                    if (existingDto == null || existingDto.PartidoPoliticoId != usuario.PartidoPoliticoId.Value)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    await _service.ActivarCandidatoAsync(id);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;

                    return RedirectToAction(nameof(Index));
                }
            }

            public async Task<IActionResult> Desactivar(int id)
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
                    var existingDto = await _service.GetByIdAsync(id);
                    if (existingDto == null || existingDto.PartidoPoliticoId != usuario.PartidoPoliticoId.Value)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    await _service.DesactivarCandidatoAsync(id);

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