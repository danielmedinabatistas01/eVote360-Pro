using eVote360Pro.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class ResultadoElectoralController : Controller
    {
        private readonly IResultadoElectoralService _resultadoElectoralService;

        public ResultadoElectoralController(IResultadoElectoralService resultadoElectoralService)
        {
            _resultadoElectoralService = resultadoElectoralService;
        }

        public async Task<IActionResult> Index(int eleccionId)
        {
            var resultados = await _resultadoElectoralService.GetResultadosByEleccionIdAsync(eleccionId);

            if (resultados == null)
                return RedirectToAction("Index", "Eleccion");

            return View(resultados);
        }
    }
}