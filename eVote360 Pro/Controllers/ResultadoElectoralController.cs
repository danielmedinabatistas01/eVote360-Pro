using eVote360Pro.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class ResultadoElectoralController : Controller
    {
        private readonly IResultadoElectoralService _resultadoElectoralService;
        private readonly IUserSession _userSession;

        public ResultadoElectoralController(
            IResultadoElectoralService resultadoElectoralService,
            IUserSession userSession)
        {
            _resultadoElectoralService = resultadoElectoralService;
            _userSession = userSession;
        }

        public async Task<IActionResult> Index(int eleccionId)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            var resultados =
                await _resultadoElectoralService
                    .GetResultadosByEleccionIdAsync(eleccionId);

            if (resultados == null)
                return RedirectToAction("Index", "Eleccion");

            return View(resultados);
        }
    }
}