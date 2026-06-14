using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.ResultadoElectoral;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class ResultadoElectoralService : IResultadoElectoralService
    {
        private readonly IVotoDetalleRepository _votoDetalleRepository;

        public ResultadoElectoralService(IVotoDetalleRepository votoDetalleRepository)
        {
            _votoDetalleRepository = votoDetalleRepository;
        }

        public async Task<List<ResultadoElectoralViewModel>> GetResultadosPorEleccionAsync(int eleccionId)
        {
            var detalles = await _votoDetalleRepository.GetByEleccionIdAsync(eleccionId);

            var totalVotos = detalles.Count;

            if (totalVotos == 0)
                return new List<ResultadoElectoralViewModel>();

            var resultados = detalles
                .GroupBy(x => new { x.PuestoElectivoId, x.CandidatoId })
                .Select(g => new ResultadoElectoralViewModel
                {
                    PuestoElectivoId = g.Key.PuestoElectivoId,
                    CandidatoId = g.Key.CandidatoId,
                    NombreCandidato = g.Key.CandidatoId == null ? "Ninguno" : $"Candidato {g.Key.CandidatoId}",
                    CantidadVotos = g.Count(),
                    Porcentaje = Math.Round((decimal)g.Count() * 100 / totalVotos, 2)
                })
                .ToList();

            foreach (var grupo in resultados.GroupBy(x => x.PuestoElectivoId))
            {
                var mayorCantidad = grupo.Max(x => x.CantidadVotos);

                foreach (var item in grupo.Where(x => x.CantidadVotos == mayorCantidad))
                {
                    item.EsGanador = true;
                }
            }

            return resultados;
        }
    }
}