using eVote360Pro.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace eVote360_Pro.Controllers
{
    public class ProcesoVotacionController : Controller
    {
        private readonly IProcesoVotacionService _service;

        public ProcesoVotacionController(
            IProcesoVotacionService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ValidarCedula(
            string cedula)
        {
            bool valido =
                await _service
                    .ValidarCedulaAsync(cedula);

            if (!valido)
            {
                ModelState.AddModelError(
                    "",
                    "La cédula no existe en el sistema.");

                return View("Index");
            }

            TempData["Cedula"] = cedula;

            return RedirectToAction(
                nameof(ValidacionOcr));
        }

        [HttpGet]
        public IActionResult ValidacionOcr()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ValidacionOcr(
            IFormFile imagenCedula)
        {
            if (imagenCedula == null ||
                imagenCedula.Length == 0)
            {
                ModelState.AddModelError(
                    "",
                    "Debe seleccionar una imagen.");

                return View();
            }

            string? cedula =
                TempData["Cedula"]?.ToString();

            if (string.IsNullOrEmpty(cedula))
            {
                return RedirectToAction(
                    nameof(Index));
            }

            TempData["Cedula"] = cedula;

            string uploadsFolder =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads");

            if (!Directory.Exists(
                uploadsFolder))
            {
                Directory.CreateDirectory(
                    uploadsFolder);
            }

            string fileName =
                Guid.NewGuid() +
                Path.GetExtension(
                    imagenCedula.FileName);

            string rutaImagen =
                Path.Combine(
                    uploadsFolder,
                    fileName);

            using (var stream =
                new FileStream(
                    rutaImagen,
                    FileMode.Create))
            {
                await imagenCedula
                    .CopyToAsync(stream);
            }

            bool coincide =
                await _service
                    .ValidarIdentidadOcrAsync(
                        cedula,
                        rutaImagen);

            if (!coincide)
            {
                ModelState.AddModelError(
                    "",
                    "La imagen no coincide con la cédula ingresada.");

                return View();
            }

            return RedirectToAction(
                nameof(Codigo));
        }

        [HttpGet]
        public IActionResult Codigo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Codigo(
            int ciudadanoId,
            int eleccionId,
            string codigo)
        {
            bool valido =
                await _service
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

            return RedirectToAction(
                nameof(Puestos));
        }

        [HttpGet]
        public IActionResult Puestos()
        {
            return View();
        }
    }
}