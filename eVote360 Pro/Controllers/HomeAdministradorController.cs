using eVote360Pro.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eVote360_Pro.Controllers
{
    public class HomeAdministradorController : Controller
    {
        private readonly IHomeAdministradorService _homeAdministradorService;

        public HomeAdministradorController(IHomeAdministradorService homeAdministradorService)
        {
            _homeAdministradorService = homeAdministradorService;
        }

        public async Task<IActionResult> Index(int? anio)
        {
            int anioConsulta = anio ?? DateTime.Now.Year;

            var resumen = await _homeAdministradorService.GetResumenByAnioAsync(anioConsulta);

            return View(resumen);
        }
    }
}