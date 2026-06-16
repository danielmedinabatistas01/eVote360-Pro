using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
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
                resumenes.Add(new ResumenEleccionDTO
                {
                    EleccionId = eleccion.Id,
                    NombreEleccion = eleccion.Nombre,
                    FechaRealizacion = eleccion.FechaRealizacion,
                    Estado = eleccion.EstadoEleccion.ToString(),
                    TotalCiudadanosQueVotaron =
                        await _votoRepository.CountCiudadanosVotaronAsync(eleccion.Id)
                });
            }

            return new HomeAdministradorDTO
            {
                Anio = anio,
                AniosDisponibles = aniosDisponibles,
                Resumenes = resumenes
            };
        }
    }
}