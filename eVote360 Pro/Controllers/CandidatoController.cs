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
            private readonly ICandidatoService
                _service;

            private readonly IMapper
                _mapper;

            public CandidatoController(
                ICandidatoService service,
                IMapper mapper)
            {
                _service = service;
                _mapper = mapper;
            }

            public async Task<IActionResult>
                Index()
            {
                var candidatos =
                    await _service
                        .GetAllAsync();

                var vm =
                    _mapper.Map<
                        List<CandidatoViewModel>>
                        (candidatos);

                return View(vm);
            }

            public IActionResult Create()
            {
                return View(
                    new SaveCandidatoViewModel());
            }

        [HttpPost]
        public async Task<IActionResult>
        Create(
        SaveCandidatoViewModel vm)
        {
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

                string extension =
                    Path.GetExtension(
                        vm.Foto.FileName)
                        .ToLower();

                string[] permitidas =
                {
        ".jpg",
        ".jpeg",
        ".png"
    };

                if (!permitidas.Contains(
                    extension))
                {
                    ModelState.AddModelError(
                        "",
                        "La foto del candidato debe ser una imagen válida.");

                    return View(vm);
                }

                string? fotoUrl =
                    FileManager.Upload(
                        vm.Foto,
                        0,
                        "candidatos");

                var dto =
                    _mapper.Map<CandidatoDto>(vm);

                dto.FotoUrl =
                    fotoUrl;

                await _service.AddAsync(dto);

                return RedirectToAction(
                    nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    ex.Message);

                return View(vm);
            }
        }

            public async Task<IActionResult>
                Edit(
                    int id)
            {
                var dto =
                    await _service
                        .GetByIdAsync(id);

                if (dto == null)
                {
                    return RedirectToAction(
                        nameof(Index));
                }

                var vm =
                    _mapper.Map<
                        SaveCandidatoViewModel>
                        (dto);

                return View(vm);
            }

            [HttpPost]
            public async Task<IActionResult>
                Edit(
                    SaveCandidatoViewModel vm)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return View(vm);
                    }

                                string? fotoUrl =
                    FileManager.Upload(
                        vm.Foto,
                        vm.Id,
                        "candidatos",
                        true,
                        vm.FotoUrl);

                    var dto =
                        _mapper.Map<CandidatoDto>(vm);

                    dto.FotoUrl =
                        fotoUrl;

                    await _service.UpdateAsync(
                        vm.Id,
                        dto);

                    return RedirectToAction(
                        nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(
                        "",
                        ex.Message);

                    return View(vm);
                }
            }

            public async Task<IActionResult>
                Delete(
                    int id)
            {
                try
                {
                    await _service.DeleteAsync(id);

                    FileManager.Delete(
                        id,
                        "candidatos");

                    return RedirectToAction(
                        nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] =
                        ex.Message;

                    return RedirectToAction(
                        nameof(Index));
                }
            }

            public async Task<IActionResult>
                Activar(
                    int id)
            {
                try
                {
                    await _service
                        .ActivarCandidatoAsync(id);

                    return RedirectToAction(
                        nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] =
                        ex.Message;

                    return RedirectToAction(
                        nameof(Index));
                }
            }

            public async Task<IActionResult>
                Desactivar(
                    int id)
            {
                try
                {
                    await _service
                        .DesactivarCandidatoAsync(id);

                    return RedirectToAction(
                        nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] =
                        ex.Message;

                    return RedirectToAction(
                        nameof(Index));
                }
            }
        }
    }