using eVote360Pro.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class HomeAdministradorController : Controller
    {
        private readonly IHomeAdministradorService _homeAdministradorService;
        private readonly IUserSession _userSession;

        public HomeAdministradorController(
            IHomeAdministradorService homeAdministradorService,
            IUserSession userSession)
        {
            _homeAdministradorService = homeAdministradorService;
            _userSession = userSession;
        }

        public async Task<IActionResult> Index(int? anio)
        {
            if (!_userSession.HasUser())
                return RedirectToAction("Index", "Login");

            if (!_userSession.IsAdmin())
                return RedirectToAction("AccessDenied", "Login");

            int anioConsulta = anio ?? DateTime.Now.Year;

            var resumen = await _homeAdministradorService.GetResumenByAnioAsync(anioConsulta);

            return View(resumen);
        }
    }
}