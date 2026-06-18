using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.HomeAdministrador;
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

            var dto = await _homeAdministradorService.GetResumenByAnioAsync(anioConsulta);

            var vm = new HomeAdministradorViewModel
            {
                Anio = dto.AnioSeleccionado,
                AniosDisponibles = dto.AniosDisponibles,
                Resumenes = dto.Resumenes.Select(x => new ResumenEleccionViewModel
                {
                    EleccionId = x.EleccionId,
                    NombreEleccion = x.NombreEleccion,
                    FechaRealizacion = x.FechaRealizacion,
                    Estado = x.Estado,
                    TotalPartidos = x.CantidadPartidosParticipantes,
                    TotalCandidatos = x.CantidadCandidatosParticipantes,
                    TotalCiudadanosQueVotaron = x.CantidadCiudadanosVotaron
                }).ToList()
            };

            return View(vm);
        }
    }
}