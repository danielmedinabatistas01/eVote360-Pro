using eVote360_Pro.Helpers;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.Ocr;
using eVote360Pro.Core.Application.ViewModels.Voto;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class ProcesoVotacionController
        : Controller
    {
        private readonly IProcesoVotacionService
            _procesoService;

        private readonly IVotoService
            _votoService;

        private readonly IVotoDetalleService
            _votoDetalleService;

        public ProcesoVotacionController(
            IProcesoVotacionService procesoService,
            IVotoService votoService,
            IVotoDetalleService votoDetalleService)
        {
            _procesoService =
                procesoService;

            _votoService =
                votoService;

            _votoDetalleService =
                votoDetalleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>
            ValidarCedula(
                string cedula)
        {
            try
            {
                bool existe =
                    await _procesoService
                        .ValidarCedulaAsync(
                            cedula);

                if (!existe)
                {
                    ModelState.AddModelError(
                        "",
                        "La cédula no se encuentra registrada.");

                    return View("Index");
                }

                TempData["Cedula"] =
                    cedula;

                return RedirectToAction(
                    nameof(ValidacionOcr));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    ex.Message);

                return View("Index");
            }
        }

        public IActionResult
            ValidacionOcr()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>
    ValidacionOcr(
        OcrViewModel vm)
        {
            try
            {
                string? ruta =
                    FileManager.Upload(
                        vm.ImagenCedula,
                        0,
                        "cedulas");

                string rutaFisica =
                    Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        ruta);

                bool coincide =
                    await _procesoService
                        .ValidarIdentidadOcrAsync(
                            vm.Cedula,
                            rutaFisica);

                if (!coincide)
                {
                    ModelState.AddModelError(
                        "",
                        "La cédula no coincide.");

                    return View(vm);
                }

                return RedirectToAction(
                    nameof(Codigo));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    ex.Message);

                return View(vm);
            }
        }

        public IActionResult
            Codigo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>
            Codigo(
                int ciudadanoId,
                int eleccionId,
                string codigo)
        {
            try
            {
                bool valido =
                    await _procesoService
                        .ValidarCodigoAsync(
                            ciudadanoId,
                            eleccionId,
                            codigo);

                if (!valido)
                {
                    ModelState.AddModelError(
                        "",
                        "Código inválido o expirado.");

                    return View();
                }

                TempData["CiudadanoId"] =
                    ciudadanoId;

                TempData["EleccionId"] =
                    eleccionId;

                return RedirectToAction(
                    nameof(Votar));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    ex.Message);

                return View();
            }
        }

        public IActionResult
            Votar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>
            Votar(
                VotoDto dto)
        {
            try
            {
                bool puedeVotar =
                    await _votoService
                        .PuedeVotarAsync(
                            dto.CiudadanoId,
                            dto.EleccionId);

                if (!puedeVotar)
                {
                    ModelState.AddModelError(
                        "",
                        "El ciudadano ya votó en esta elección.");

                    return View(dto);
                }

                await _votoService
                    .RegistrarVotoAsync(
                        dto);

                return RedirectToAction(
                    nameof(Confirmacion));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    ex.Message);

                return View(dto);
            }
        }

        public IActionResult
            Confirmacion()
        {
            return View();
        }
    

        [HttpPost]
            public async Task<IActionResult>
        Votar(
            EmitirVotoViewModel vm)
            {
                try
                {
                    var votoId =
                        await _votoService
                            .RegistrarVotoAsync(
                                new VotoDto
                                {
                                    CiudadanoId =
                                        vm.CiudadanoId,

                                    EleccionId =
                                        vm.EleccionId
                                });

                    foreach (var item in vm.Selecciones)
                    {
                        await _votoDetalleService
                            .CreateAsync(
                                new VotoDetalleDTO
                                {
                                    VotoId = votoId,

                                    PuestoElectivoId =
                                        item.PuestoElectivoId,

                                    CandidatoId =
                                        item.CandidatoId
                                });
                    }

                    return RedirectToAction(
                        nameof(Confirmacion));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(
                        "",
                        ex.Message);

                    return View(vm);
                }
        }
    }
}