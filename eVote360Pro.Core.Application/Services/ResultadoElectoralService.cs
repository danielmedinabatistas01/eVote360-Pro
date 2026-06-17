using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
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

        public async Task<List<ResultadoElectoralDTO>> GetResultadosByEleccionIdAsync(int eleccionId)
        {
            var detalles = await _votoDetalleRepository.GetByEleccionIdAsync(eleccionId);

            var resultados = detalles
        .GroupBy(x => new
        {
         x.PuestoElectivoId,
         NombrePuesto = x.PuestoElectivo != null
             ? x.PuestoElectivo.Nombre
             : $"Puesto {x.PuestoElectivoId}",
         x.CandidatoId,
         NombreCandidato = x.Candidato != null
             ? x.Candidato.Nombre + " " + x.Candidato.Apellido
             : "Ninguno"
        })
        .Select(g => new ResultadoElectoralDTO
        {
         EleccionId = eleccionId,
         PuestoElectivoId = g.Key.PuestoElectivoId,
         NombrePuestoElectivo = g.Key.NombrePuesto,
         CandidatoId = g.Key.CandidatoId,
         NombreCandidato = g.Key.NombreCandidato,
         CantidadVotos = g.Count(),
         Porcentaje = 0,
         EsEmpate = false
          })
          .ToList();

            foreach (var grupo in resultados.GroupBy(x => x.PuestoElectivoId))
            {
                var totalVotosPorPuesto = grupo.Sum(x => x.CantidadVotos);
                var maxVotos = grupo.Max(x => x.CantidadVotos);
                var cantidadEmpatados = grupo.Count(x => x.CantidadVotos == maxVotos);

                foreach (var resultado in grupo)
                {
                    resultado.Porcentaje = totalVotosPorPuesto == 0
                        ? 0
                        : Math.Round((decimal)resultado.CantidadVotos * 100 / totalVotosPorPuesto, 2);

                    resultado.EsEmpate =
                        resultado.CantidadVotos == maxVotos &&
                        cantidadEmpatados > 1;
                }
            }

            return resultados;
        }
    }
}