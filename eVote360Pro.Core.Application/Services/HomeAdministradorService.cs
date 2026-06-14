using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.HomeAdministrador;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class HomeAdministradorService : IHomeAdministradorService
    {
        private readonly IEleccionRepository _eleccionRepository;
        private readonly IVotoRepository _votoRepository;

        public HomeAdministradorService(
            IEleccionRepository eleccionRepository,
            IVotoRepository votoRepository)
        {
            _eleccionRepository = eleccionRepository;
            _votoRepository = votoRepository;
        }

        public async Task<HomeAdministradorViewModel> GetResumenPorAnioAsync(int? anio)
        {
            var elecciones = await _eleccionRepository.GetAllList();

            var aniosDisponibles = elecciones
                .Select(x => x.FechaRealizacion.Year)
                .Distinct()
                .OrderByDescending(x => x)
                .ToList();

            int? anioSeleccionado = anio ?? aniosDisponibles.FirstOrDefault();

            var eleccionesFiltradas = elecciones
                .Where(x => x.FechaRealizacion.Year == anioSeleccionado)
                .OrderByDescending(x => x.FechaRealizacion)
                .ToList();

            var resumenes = new List<ResumenEleccionViewModel>();

            foreach (var eleccion in eleccionesFiltradas)
            {
                resumenes.Add(new ResumenEleccionViewModel
                {
                    EleccionId = eleccion.Id,
                    NombreEleccion = eleccion.Nombre,
                    FechaRealizacion = eleccion.FechaRealizacion,
                    CantidadPartidosParticipantes = 0,
                    CantidadCandidatosParticipantes = 0,
                    CantidadCiudadanosVotaron =
                        await _votoRepository.CountCiudadanosVotaronAsync(eleccion.Id)
                });
            }

            return new HomeAdministradorViewModel
            {
                AniosDisponibles = aniosDisponibles,
                AnioSeleccionado = anioSeleccionado,
                Resumenes = resumenes
            };
        }
    }
}