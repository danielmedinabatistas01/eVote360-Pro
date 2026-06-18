using eVote360Pro.Core.Application.ViewModels.ProcesoVotacion;
using Microsoft.AspNetCore.Mvc;

namespace eVote360Pro.Web.Controllers
{
    public class ProcesoVotacionController : Controller
    {
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
            var vm = new ProcesoVotacionOcrViewModel
            {
                DocumentoIdentidad = TempData["DocumentoIdentidad"]?.ToString()
            };

            TempData.Keep("DocumentoIdentidad");

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ValidacionOcr(ProcesoVotacionOcrViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            TempData["DocumentoIdentidad"] = vm.DocumentoIdentidad;

            return RedirectToAction(nameof(Codigo));
        }

        [HttpGet]
        public IActionResult Codigo()
        {
            var vm = new ProcesoVotacionCodigoViewModel
            {
                DocumentoIdentidad = TempData["DocumentoIdentidad"]?.ToString()
            };

            TempData.Keep("DocumentoIdentidad");

            return View(vm);
        }
    }
}