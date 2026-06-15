using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.ResultadoElectoral;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class ResultadoElectoralService : IResultadoElectoralService
    {
        private readonly IEleccionRepository _eleccionRepository;
        private readonly IVotoDetalleRepository _votoDetalleRepository;

        public ResultadoElectoralService(
            IEleccionRepository eleccionRepository,
            IVotoDetalleRepository votoDetalleRepository)
        {
            _eleccionRepository = eleccionRepository;
            _votoDetalleRepository = votoDetalleRepository;
        }

        public async Task<ResultadoElectoralIndexViewModel?> GetResultadosByEleccionIdAsync(int eleccionId)
        {
            var eleccion = await _eleccionRepository.GetById(eleccionId);

            if (eleccion == null)
                return null;

            var detalles = await _votoDetalleRepository.GetByEleccionIdAsync(eleccionId);

            var totalVotos = detalles.Count;

            var resultados = detalles
                .GroupBy(x => new { x.PuestoElectivoId, x.CandidatoId })
                .Select(g => new ResultadoPorPuestoViewModel
                {
                    PuestoElectivoId = g.Key.PuestoElectivoId,
                    NombrePuesto = $"Puesto {g.Key.PuestoElectivoId}",
                    CandidatoId = g.Key.CandidatoId,
                    NombreCandidato = g.Key.CandidatoId == null
                        ? "Ninguno"
                        : $"Candidato {g.Key.CandidatoId}",
                    CantidadVotos = g.Count(),
                    Porcentaje = totalVotos == 0
                        ? 0
                        : Math.Round((decimal)g.Count() * 100 / totalVotos, 2)
                })
                .ToList();

            foreach (var grupo in resultados.GroupBy(x => x.PuestoElectivoId))
            {
                var maxVotos = grupo.Max(x => x.CantidadVotos);

                foreach (var resultado in grupo)
                {
                    resultado.EsEmpate =
                        resultado.CantidadVotos == maxVotos &&
                        grupo.Count(x => x.CantidadVotos == maxVotos) > 1;
                }
            }

            return new ResultadoElectoralIndexViewModel
            {
                EleccionId = eleccion.Id,
                NombreEleccion = eleccion.Nombre,
                FechaRealizacion = eleccion.FechaRealizacion,
                ResultadosPorPuesto = resultados
            };
        }
    }
}