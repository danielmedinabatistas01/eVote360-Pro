using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Services
{
    public class ResultadoElectoralService : IResultadoElectoralService
    {
        private readonly IVotoDetalleRepository _votoDetalleRepository;
        private readonly IEleccionPuestoElectivoRepository _eleccionPuestoRepository;
        private readonly IAsignacionCandidatoRepository _asignacionRepository;

        public ResultadoElectoralService(
            IVotoDetalleRepository votoDetalleRepository,
            IEleccionPuestoElectivoRepository eleccionPuestoRepository,
            IAsignacionCandidatoRepository asignacionRepository)
        {
            _votoDetalleRepository = votoDetalleRepository;
            _eleccionPuestoRepository = eleccionPuestoRepository;
            _asignacionRepository = asignacionRepository;
        }

        public async Task<List<ResultadoElectoralDTO>> GetResultadosByEleccionIdAsync(int eleccionId)
        {
            var puestosEleccion = await _eleccionPuestoRepository.GetAllQueryWithInclude(new List<string> { "PuestoElectivo" })
                .Where(x => x.EleccionId == eleccionId)
                .ToListAsync();

            var asignaciones = await _asignacionRepository.ObtenerPorEleccionAsync(eleccionId);
            var detalles = await _votoDetalleRepository.GetByEleccionIdAsync(eleccionId);

            var resultados = new List<ResultadoElectoralDTO>();

            foreach (var pe in puestosEleccion)
            {
                var puestoId = pe.PuestoElectivoId;
                var puestoNombre = pe.PuestoElectivo?.Nombre ?? $"Puesto {puestoId}";

                // Candidates assigned to this post
                var postAsignaciones = asignaciones.Where(a => a.PuestoElectivoId == puestoId).ToList();

                // Group details by CandidateId for this post
                var postDetalles = detalles.Where(d => d.PuestoElectivoId == puestoId).ToList();

                // Add each assigned candidate
                foreach (var assign in postAsignaciones)
                {
                    var candidatoId = assign.CandidatoId;
                    var candidatoNombre = assign.Candidato != null 
                        ? $"{assign.Candidato.Nombre} {assign.Candidato.Apellido}" 
                        : $"Candidato {candidatoId}";

                    var vCount = postDetalles.Count(d => d.CandidatoId == candidatoId);

                    resultados.Add(new ResultadoElectoralDTO
                    {
                        EleccionId = eleccionId,
                        PuestoElectivoId = puestoId,
                        NombrePuestoElectivo = puestoNombre,
                        CandidatoId = candidatoId,
                        NombreCandidato = candidatoNombre,
                        CantidadVotos = vCount,
                        Porcentaje = 0,
                        EsEmpate = false
                    });
                }

                // Add "Ninguno"
                var ningunoCount = postDetalles.Count(d => d.CandidatoId == null);
                resultados.Add(new ResultadoElectoralDTO
                {
                    EleccionId = eleccionId,
                    PuestoElectivoId = puestoId,
                    NombrePuestoElectivo = puestoNombre,
                    CandidatoId = null,
                    NombreCandidato = "Ninguno",
                    CantidadVotos = ningunoCount,
                    Porcentaje = 0,
                    EsEmpate = false
                });
            }

            // Now calculate percentages and tie states for each post
            foreach (var grupo in resultados.GroupBy(x => x.PuestoElectivoId))
            {
                var totalVotosPorPuesto = grupo.Sum(x => x.CantidadVotos);
                var maxVotos = grupo.Max(x => x.CantidadVotos);
                var cantidadEmpatados = grupo.Count(x => x.CantidadVotos == maxVotos);
                bool esEmpateValido = maxVotos > 0;

                foreach (var resultado in grupo)
                {
                    resultado.Porcentaje = totalVotosPorPuesto == 0
                        ? 0
                        : Math.Round((decimal)resultado.CantidadVotos * 100 / totalVotosPorPuesto, 2);

                    resultado.EsEmpate = esEmpateValido &&
                        resultado.CantidadVotos == maxVotos &&
                        cantidadEmpatados > 1;

                    resultado.EsGanador = esEmpateValido &&
                        resultado.CantidadVotos == maxVotos &&
                        cantidadEmpatados == 1;
                }
            }

            return resultados;
        }
    }
}