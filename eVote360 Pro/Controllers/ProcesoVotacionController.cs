using eVote360Pro.Core.Application.DTOs.Email;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.ProcesoVotacion;
using Microsoft.AspNetCore.Mvc;

namespace eVote360Pro.Web.Controllers
{
    public class ProcesoVotacionController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IPuestoElectivoService _puestoElectivoService;

        public ProcesoVotacionController(
            IEmailService emailService,
            IPuestoElectivoService puestoElectivoService)
        {
            _emailService = emailService;
            _puestoElectivoService = puestoElectivoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new IniciarVotacionViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IniciarVotacionViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            TempData["DocumentoIdentidad"] = vm.DocumentoIdentidad;

            return RedirectToAction(nameof(ValidacionOcr));
        }

        [HttpGet]
        public IActionResult ValidacionOcr()
        {
            var documento = TempData["DocumentoIdentidad"]?.ToString();

            if (string.IsNullOrWhiteSpace(documento))
                return RedirectToAction(nameof(Index));

            TempData.Keep("DocumentoIdentidad");

            return View(new ProcesoVotacionOcrViewModel
            {
                DocumentoIdentidad = documento
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidacionOcr(ProcesoVotacionOcrViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var codigo = new Random().Next(100000, 999999).ToString();

            TempData["DocumentoIdentidad"] = vm.DocumentoIdentidad;
            TempData["CodigoVerificacion"] = codigo;

            await _emailService.SendAsync(new EmailRequestDTO
            {
                To = "franklinjosebaex@gmail.com",
                Subject = "Código de verificación - eVote360 Pro",
                HtmlBody = $@"
                    <h2>eVote360 Pro</h2>
                    <p>Su código de verificación es:</p>
                    <h1>{codigo}</h1>
                    <p>Este código es personal y no debe compartirlo.</p>"
            });

            return RedirectToAction(nameof(Codigo));
        }

        [HttpGet]
        public IActionResult Codigo()
        {
            var documento = TempData["DocumentoIdentidad"]?.ToString();

            if (string.IsNullOrWhiteSpace(documento))
                return RedirectToAction(nameof(Index));

            TempData.Keep("DocumentoIdentidad");
            TempData.Keep("CodigoVerificacion");

            return View(new ProcesoVotacionCodigoViewModel
            {
                DocumentoIdentidad = documento
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Codigo(ProcesoVotacionCodigoViewModel vm)
        {
            var codigoReal = TempData["CodigoVerificacion"]?.ToString();

            TempData.Keep("DocumentoIdentidad");
            TempData.Keep("CodigoVerificacion");

            if (!ModelState.IsValid)
                return View(vm);

            if (string.IsNullOrWhiteSpace(codigoReal))
            {
                TempData["ErrorMessage"] = "El código expiró. Debe iniciar el proceso nuevamente.";
                return RedirectToAction(nameof(Index));
            }

            if (vm.Codigo != codigoReal)
            {
                TempData["ErrorMessage"] = "El código ingresado no es válido.";
                return View(vm);
            }

            TempData["DocumentoIdentidad"] = vm.DocumentoIdentidad;

            return RedirectToAction(nameof(Puestos));
        }
        [HttpGet]
        public async Task<IActionResult> Puestos()
        {
            var documento = TempData["DocumentoIdentidad"]?.ToString();

            if (string.IsNullOrWhiteSpace(documento))
                return RedirectToAction(nameof(Index));

            TempData.Keep("DocumentoIdentidad");

            var puestosDto = await _puestoElectivoService.GetAllAsync();

            var puestos = puestosDto
                .Where(p => p.EsActivo)
                .Select(p => new PuestoDisponibleViewModel
                {
                    PuestoElectivoId = p.Id,
                    NombrePuesto = p.Nombre,
                    CantidadPartidosParticipantes = 0,
                    CantidadCandidatosReales = 0,
                    YaSeleccionado = TempData[$"Puesto_{p.Id}"] != null
                })
                .ToList();

            return View(puestos);
        }

        [HttpGet]
        public IActionResult SeleccionCandidato(int puestoId)
        {
            TempData["ErrorMessage"] = "El módulo de candidatos todavía no está conectado al proceso de votación.";
            return RedirectToAction(nameof(Puestos));
        }
    }
}