using eVote360_Pro.Helpers;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.DTOs.Email;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.Ocr;
using eVote360Pro.Core.Application.ViewModels.Voto;
using eVote360Pro.Core.Application.ViewModels.ProcesoVotacion;
using eVote360Pro.Core.Application.ViewModels.CodigoVerificacion;
using eVote360Pro.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eVote360Pro.Web.Controllers
{
    public class ProcesoVotacionController : Controller
    {
        private readonly IProcesoVotacionService _procesoService;
        private readonly IVotoService _votoService;
        private readonly IVotoDetalleService _votoDetalleService;
        private readonly IEleccionService _eleccionService;
        private readonly ICiudadanoRepository _ciudadanoRepository;
        private readonly IEmailService _emailService;
        private readonly IAsignacionCandidatoService _asignacionCandidatoService;
        private readonly IPuestoElectivoService _puestoElectivoService;
        private readonly ICandidatoService _candidatoService;
        private readonly IPartidoPoliticoService _partidoPoliticoService;
        private readonly IEleccionPuestoElectivoService _eleccionPuestoElectivoService;

        public ProcesoVotacionController(
            IProcesoVotacionService procesoService,
            IVotoService votoService,
            IVotoDetalleService votoDetalleService,
            IEleccionService eleccionService,
            ICiudadanoRepository ciudadanoRepository,
            IEmailService emailService,
            IAsignacionCandidatoService asignacionCandidatoService,
            IPuestoElectivoService puestoElectivoService,
            ICandidatoService candidatoService,
            IPartidoPoliticoService partidoPoliticoService,
            IEleccionPuestoElectivoService eleccionPuestoElectivoService)
        {
            _procesoService = procesoService;
            _votoService = votoService;
            _votoDetalleService = votoDetalleService;
            _eleccionService = eleccionService;
            _ciudadanoRepository = ciudadanoRepository;
            _emailService = emailService;
            _asignacionCandidatoService = asignacionCandidatoService;
            _puestoElectivoService = puestoElectivoService;
            _candidatoService = candidatoService;
            _partidoPoliticoService = partidoPoliticoService;
            _eleccionPuestoElectivoService = eleccionPuestoElectivoService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            bool activeElection = await _eleccionService.ExisteEleccionActivaAsync();
            if (!activeElection)
            {
                TempData["ErrorMessage"] = "No hay una elección activa en este momento. Vuelva más tarde.";
            }

            return View(new eVote360Pro.Core.Application.ViewModels.ProcesoVotacion.IniciarVotacionViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IniciarVotacionViewModel vm)
        {
            vm.DocumentoIdentidad = vm.DocumentoIdentidad?.Trim() ?? string.Empty;

            ModelState.Clear();

            if (!TryValidateModel(vm))
            {
                ModelState.AddModelError("", "Debe ingresar un documento de identidad válido.");
                return View(vm);
            }

            try
            {
                var activeElection = await _eleccionService.GetEleccionActivaAsync();

                if (activeElection == null)
                {
                    ModelState.AddModelError("", "No hay una elección activa en este momento.");
                    return View(vm);
                }

                var ciudadano = await _ciudadanoRepository.GetByCedulaAsync(vm.DocumentoIdentidad);

                if (ciudadano == null)
                {
                    ModelState.AddModelError("", "La cédula ingresada no se encuentra registrada.");
                    return View(vm);
                }

                if (!ciudadano.EsActivo)
                {
                    ModelState.AddModelError("", "Este ciudadano se encuentra inactivo.");
                    return View(vm);
                }

                bool yaVoto = await _votoService.CiudadanoYaVotoAsync(ciudadano.Id, activeElection.Id);

                if (yaVoto)
                {
                    ModelState.AddModelError("", "El ciudadano ya votó en esta elección.");
                    return View(vm);
                }

                HttpContext.Session.SetInt32("CiudadanoId", ciudadano.Id);
                HttpContext.Session.SetString("CiudadanoEmail", ciudadano.CorreoElectronico ?? string.Empty);
                HttpContext.Session.SetString("CiudadanoNombre", $"{ciudadano.Nombre} {ciudadano.Apellido}");
                HttpContext.Session.SetString("Cedula", vm.DocumentoIdentidad);
                HttpContext.Session.SetInt32("EleccionId", activeElection.Id);
                HttpContext.Session.SetString("CodigoVerificado", "false");

                return RedirectToAction(nameof(ValidacionOcr));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }


        [HttpGet]
        public IActionResult ValidacionOcr()
        {
            string? cedula = HttpContext.Session.GetString("Cedula");
            if (string.IsNullOrEmpty(cedula))
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new OcrViewModel { Cedula = cedula });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidacionOcr(OcrViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            int? ciudadanoId = HttpContext.Session.GetInt32("CiudadanoId");
            int? eleccionId = HttpContext.Session.GetInt32("EleccionId");
            string? email = HttpContext.Session.GetString("CiudadanoEmail");
            string? nombre = HttpContext.Session.GetString("CiudadanoNombre");

            if (ciudadanoId == null || eleccionId == null || string.IsNullOrEmpty(email))
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                if (vm.ImagenCedula == null || vm.ImagenCedula.Length == 0)
                {
                    ModelState.AddModelError("", "Debe cargar una imagen válida.");
                    return View(vm);
                }

                using var memoryStream = new MemoryStream();
                await vm.ImagenCedula.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();

                bool coincide = await _procesoService.ValidarIdentidadOcrAsync(vm.Cedula, imageBytes);
                if (!coincide)
                {
                    ModelState.AddModelError("", "La cédula en la imagen no coincide con la ingresada.");
                    return View(vm);
                }


                string code = await _procesoService.GenerarCodigoAsync(ciudadanoId.Value, eleccionId.Value);

                // Send email
                await _emailService.SendAsync(new EmailRequestDTO
                {
                    To = email,
                    Subject = "Código de verificación para votar",
                    HtmlBody = $@"
                        <h3>Hola {nombre},</h3>
                        <p>Su código de verificación para continuar con el proceso de votación es:</p>
                        <h2 style='color:#0d6efd; letter-spacing: 5px;'>{code}</h2>
                        <p>Este código tendrá una vigencia de 5 minutos.</p>
                        <p>Si usted no inició este proceso, ignore este mensaje.</p>"
                });

                return RedirectToAction(nameof(Codigo));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }


        [HttpGet]
        public IActionResult Codigo()
        {
            int? ciudadanoId = HttpContext.Session.GetInt32("CiudadanoId");
            int? eleccionId = HttpContext.Session.GetInt32("EleccionId");

            if (ciudadanoId == null || eleccionId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new CodigoVerificacionViewModel
            {
                CiudadanoId = ciudadanoId.Value,
                EleccionId = eleccionId.Value
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Codigo(CodigoVerificacionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                bool valido = await _procesoService.ValidarCodigoAsync(vm.CiudadanoId, vm.EleccionId, vm.Codigo);
                if (!valido)
                {
                    ModelState.AddModelError("", "Código inválido o expirado.");
                    return View(vm);
                }


                HttpContext.Session.SetString("CodigoVerificado", "true");

                return RedirectToAction(nameof(Puestos));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Puestos()
        {
            if (HttpContext.Session.GetString("CodigoVerificado") != "true")
            {
                return RedirectToAction(nameof(Index));
            }

            int? ciudadanoId = HttpContext.Session.GetInt32("CiudadanoId");
            int? eleccionId = HttpContext.Session.GetInt32("EleccionId");

            if (ciudadanoId == null || eleccionId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            bool yaVoto = await _votoService.CiudadanoYaVotoAsync(ciudadanoId.Value, eleccionId.Value);
            if (yaVoto)
            {
                TempData["ErrorMessage"] = "El ciudadano ya votó en esta elección.";
                return RedirectToAction(nameof(Index));
            }

            var eleccionPuestos = await _eleccionPuestoElectivoService.GetByEleccionIdAsync(eleccionId.Value);
            var puestosDisponibles = new List<SeleccionVotoViewModel>();

            foreach (var ep in eleccionPuestos)
            {
                var puesto = await _puestoElectivoService.GetByIdAsync(ep.PuestoElectivoId);
                if (puesto == null || !puesto.EsActivo) continue;

                puestosDisponibles.Add(new SeleccionVotoViewModel
                {
                    PuestoElectivoId = puesto.Id,
                    NombrePuesto = puesto.Nombre,
                    Candidatos = new List<SelectListItem>()
                });
            }

            var vm = new EmitirVotoViewModel
            {
                CiudadanoId = ciudadanoId.Value,
                EleccionId = eleccionId.Value,
                Selecciones = puestosDisponibles
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Puestos(EmitirVotoViewModel vm)
        {
            if (HttpContext.Session.GetString("CodigoVerificado") != "true")
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Seleccion));
        }


        [HttpGet]
        public async Task<IActionResult> Seleccion()
        {
            if (HttpContext.Session.GetString("CodigoVerificado") != "true")
            {
                return RedirectToAction(nameof(Index));
            }

            int? ciudadanoId = HttpContext.Session.GetInt32("CiudadanoId");
            int? eleccionId = HttpContext.Session.GetInt32("EleccionId");

            if (ciudadanoId == null || eleccionId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var eleccionPuestos = await _eleccionPuestoElectivoService.GetByEleccionIdAsync(eleccionId.Value);
            var assignments = await _asignacionCandidatoService.ObtenerPorEleccionAsync(eleccionId.Value);

            var vm = new EmitirVotoViewModel
            {
                CiudadanoId = ciudadanoId.Value,
                EleccionId = eleccionId.Value,
                Selecciones = new List<SeleccionVotoViewModel>()
            };

            foreach (var ep in eleccionPuestos)
            {
                var puesto = await _puestoElectivoService.GetByIdAsync(ep.PuestoElectivoId);
                if (puesto == null || !puesto.EsActivo) continue;

                var seleccion = new SeleccionVotoViewModel
                {
                    PuestoElectivoId = puesto.Id,
                    NombrePuesto = puesto.Nombre,
                    Candidatos = new List<SelectListItem>(),
                    CandidatosExtendidos = new List<CandidatoVotoViewModel>()
                };

                var postAssignments = assignments.Where(a => a.PuestoElectivoId == puesto.Id).ToList();
                foreach (var assign in postAssignments)
                {
                    var candidato = await _candidatoService.GetByIdAsync(assign.CandidatoId);
                    if (candidato == null || !candidato.Estado) continue;

                    var partido = await _partidoPoliticoService.GetByIdAsync(candidato.PartidoPoliticoId);
                    string partidoSiglas = partido != null ? partido.Siglas : "Independiente";

                    seleccion.Candidatos.Add(new SelectListItem
                    {
                        Value = candidato.Id.ToString(),
                        Text = $"{candidato.Nombre} {candidato.Apellido} ({partidoSiglas})"
                    });

                    seleccion.CandidatosExtendidos.Add(new CandidatoVotoViewModel
                    {
                        Id = candidato.Id,
                        Nombre = candidato.Nombre,
                        Apellido = candidato.Apellido,
                        FotoUrl = candidato.FotoUrl,
                        PartidoNombre = partido?.Nombre ?? "Independiente",
                        PartidoSiglas = partidoSiglas,
                        PartidoLogoUrl = partido?.LogoUrl ?? ""
                    });
                }

                vm.Selecciones.Add(seleccion);
            }

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Seleccion(EmitirVotoViewModel vm)
        {
            int? sessionCiudadanoId = HttpContext.Session.GetInt32("CiudadanoId");
            int? sessionEleccionId = HttpContext.Session.GetInt32("EleccionId");
            string? email = HttpContext.Session.GetString("CiudadanoEmail");
            string? nombre = HttpContext.Session.GetString("CiudadanoNombre");

            if (sessionCiudadanoId == null || sessionEleccionId == null || HttpContext.Session.GetString("CodigoVerificado") != "true")
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {

                vm.CiudadanoId = sessionCiudadanoId.Value;
                vm.EleccionId = sessionEleccionId.Value;

                bool puedeVotar = await _votoService.PuedeVotarAsync(vm.CiudadanoId, vm.EleccionId);
                if (!puedeVotar)
                {
                    ModelState.AddModelError("", "El ciudadano ya votó en esta elección.");
                    return View(vm);
                }

                // Register the main Voto
                var votoId = await _votoService.RegistrarVotoAsync(new VotoDto
                {
                    CiudadanoId = vm.CiudadanoId,
                    EleccionId = vm.EleccionId,
                    FechaVotacion = DateTime.Now
                });


                var summaryLines = new List<string>();
                foreach (var item in vm.Selecciones)
                {

                    await _votoDetalleService.CreateAsync(new VotoDetalleDTO
                    {
                        VotoId = votoId,
                        PuestoElectivoId = item.PuestoElectivoId,
                        CandidatoId = item.CandidatoId
                    });

                    var puesto = await _puestoElectivoService.GetByIdAsync(item.PuestoElectivoId);
                    string puestoNombre = puesto != null ? puesto.Nombre : "Puesto Desconocido";

                    if (item.CandidatoId == null)
                    {
                        summaryLines.Add($"<p><strong>Puesto:</strong> {puestoNombre}<br/><strong>Selección:</strong> Ninguno</p>");
                    }
                    else
                    {
                        var candidato = await _candidatoService.GetByIdAsync(item.CandidatoId.Value);
                        if (candidato != null)
                        {
                            var partido = await _partidoPoliticoService.GetByIdAsync(candidato.PartidoPoliticoId);
                            string partidoSiglas = partido != null ? partido.Siglas : "Independiente";
                            summaryLines.Add($"<p><strong>Puesto:</strong> {puestoNombre}<br/><strong>Selección:</strong> {candidato.Nombre} {candidato.Apellido} ({partidoSiglas})</p>");
                        }
                        else
                        {
                            summaryLines.Add($"<p><strong>Puesto:</strong> {puestoNombre}<br/><strong>Selección:</strong> Ninguno</p>");
                        }
                    }
                }


                if (!string.IsNullOrEmpty(email))
                {
                    string htmlBody = $@"
                        <h3>Hola {nombre},</h3>
                        <p>Su voto ha sido emitido exitosamente. A continuación se muestra el resumen de su boleta electoral:</p>
                        <hr/>
                        {string.Join("", summaryLines)}
                        <hr/>
                        <p>Gracias por participar en el proceso electoral.</p>";

                    await _emailService.SendAsync(new EmailRequestDTO
                    {
                        To = email,
                        Subject = "Resumen de su Voto - eVote360",
                        HtmlBody = htmlBody
                    });
                }


                HttpContext.Session.Clear();

                return RedirectToAction(nameof(Finalizar));
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }


        [HttpGet]
        public IActionResult Finalizar()
        {
            return View();
        }
    }
}