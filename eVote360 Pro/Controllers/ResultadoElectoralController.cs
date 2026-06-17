using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class ResultadoElectoralController : Controller
    {
        private readonly IResultadoElectoralService _resultadoElectoralService;
        private readonly IEleccionService _eleccionService;
        private readonly IUserSession _userSession;

        public ResultadoElectoralController(
            IResultadoElectoralService resultadoElectoralService,
            IEleccionService eleccionService,
            IUserSession userSession)
        {
            _resultadoElectoralService = resultadoElectoralService;
            _eleccionService = eleccionService;
            _userSession = userSession;
        }

        public async Task<IActionResult> Index(int eleccionId)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var eleccion = await _eleccionService.GetByIdAsync(eleccionId);

            if (eleccion == null)
                return RedirectToAction("Index", "Eleccion");

            if (eleccion.EstadoEleccion != EstadoEleccion.Finalizada)
            {
                TempData["Error"] = "Los resultados solo pueden visualizarse para elecciones finalizadas.";
                return RedirectToAction("Index", "Eleccion");
            }

            var resultados = await _resultadoElectoralService
                .GetResultadosByEleccionIdAsync(eleccionId);

            return View(resultados);
        }
    }
}