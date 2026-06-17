using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class HomeAdministradorService : IHomeAdministradorService
    {
        private readonly IEleccionRepository _eleccionRepository;
        private readonly IVotoRepository _votoRepository;
        private readonly IAsignacionCandidatoRepository _asignacionCandidatoRepository;

        public HomeAdministradorService(
            IEleccionRepository eleccionRepository,
            IVotoRepository votoRepository,
            IAsignacionCandidatoRepository asignacionCandidatoRepository)
        {
            _eleccionRepository = eleccionRepository;
            _votoRepository = votoRepository;
            _asignacionCandidatoRepository = asignacionCandidatoRepository;
        }

        public async Task<HomeAdministradorDTO> GetResumenByAnioAsync(int anio)
        {
            var elecciones = await _eleccionRepository.GetAllOrdenadasAsync();

            var aniosDisponibles = elecciones
                .Select(x => x.FechaRealizacion.Year)
                .Distinct()
                .OrderByDescending(x => x)
                .ToList();

            var eleccionesFiltradas = elecciones
                .Where(x => x.FechaRealizacion.Year == anio)
                .ToList();

            var resumenes = new List<ResumenEleccionDTO>();

            foreach (var eleccion in eleccionesFiltradas)
            {
                var asignaciones = await _asignacionCandidatoRepository
                    .ObtenerPorEleccionAsync(eleccion.Id);

                var cantidadPartidos = asignaciones
                    .Where(x => x.Candidato != null)
                    .Select(x => x.Candidato!.PartidoPoliticoId)
                    .Distinct()
                    .Count();

                var cantidadCandidatos = asignaciones
                    .Select(x => x.CandidatoId)
                    .Distinct()
                    .Count();

                var cantidadCiudadanosVotaron = await _votoRepository
                    .CountCiudadanosVotaronAsync(eleccion.Id);

                resumenes.Add(new ResumenEleccionDTO
                {
                    EleccionId = eleccion.Id,
                    NombreEleccion = eleccion.Nombre,
                    FechaRealizacion = eleccion.FechaRealizacion,
                    Estado = eleccion.EstadoEleccion.ToString(),
                    CantidadPartidosParticipantes = cantidadPartidos,
                    CantidadCandidatosParticipantes = cantidadCandidatos,
                    CantidadCiudadanosVotaron = cantidadCiudadanosVotaron
                });
            }

            return new HomeAdministradorDTO
            {
                AnioSeleccionado = anio,
                AniosDisponibles = aniosDisponibles,
                Resumenes = resumenes
            };
        }
    }
}