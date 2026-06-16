using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class AsignacionCandidatoService
        : GenericService<
            AsignacionCandidatoDto,
            AsignacionCandidato>,
          IAsignacionCandidatoService
    {
        private readonly IAsignacionCandidatoRepository
            _asignacionRepository;

        public AsignacionCandidatoService(
            IAsignacionCandidatoRepository asignacionRepository,
            IMapper mapper)
            : base(asignacionRepository, mapper)
        {
            _asignacionRepository =
                asignacionRepository;
        }

        public async Task<bool> ExisteAsignacionAsync(
            int candidatoId,
            int puestoId,
            int eleccionId)
        {
            return await _asignacionRepository
                .ExisteAsignacionAsync(
                    candidatoId,
                    puestoId,
                    eleccionId);
        }

        public async Task AsignarCandidatoAsync(
            AsignacionCandidatoDto dto)
        {
            bool existe =
                await ExisteAsignacionAsync(
                    dto.CandidatoId,
                    dto.PuestoElectivoId,
                    dto.EleccionId);

            if (existe)
            {
                throw new Exception(
                    "El candidato ya está asignado.");
            }

            var asignacion =
                _mapper.Map<AsignacionCandidato>(dto);

            await _asignacionRepository
                .AddAsync(asignacion);
        }

        public async Task<List<AsignacionCandidatoDto>>
            ObtenerPorEleccionAsync(int eleccionId)
        {
            var asignaciones =
                await _asignacionRepository
                    .ObtenerPorEleccionAsync(
                        eleccionId);

            return _mapper.Map<
                List<AsignacionCandidatoDto>>
                (asignaciones);
        }

        public async Task<List<AsignacionCandidatoDto>>
            ObtenerPorPuestoAsync(int puestoId)
        {
            var asignaciones =
                await _asignacionRepository
                    .ObtenerPorPuestoAsync(
                        puestoId);

            return _mapper.Map<
                List<AsignacionCandidatoDto>>
                (asignaciones);
        }
    }
}