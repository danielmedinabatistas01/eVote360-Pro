using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Repositories;

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

        private readonly ICandidatoRepository
            _candidatoRepository;

        private readonly IPuestoElectivoRepository
            _puestoRepository;

        private readonly IEleccionRepository
            _eleccionRepository;

        private readonly IAlianzaPoliticaRepository
            _alianzaRepository;

        public AsignacionCandidatoService(
            IAsignacionCandidatoRepository asignacionRepository,
            ICandidatoRepository candidatoRepository,
            IPuestoElectivoRepository puestoRepository,
            IEleccionRepository eleccionRepository,
            IAlianzaPoliticaRepository alianzaRepository,
            IMapper mapper)
            : base(asignacionRepository, mapper)
        {
            _asignacionRepository =
                asignacionRepository;

            _candidatoRepository =
                candidatoRepository;

            _puestoRepository =
                puestoRepository;

            _eleccionRepository =
                eleccionRepository;

            _alianzaRepository =
                alianzaRepository;
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
            AsignacionCandidatoDto dto,
            int partidoDirigenteId)
        {
            if (await _eleccionRepository
                .ExisteEleccionActivaAsync())
            {
                throw new Exception(
                    "No se puede asignar candidatos a puestos mientras exista una elección activa.");
            }

            var candidato =
                await _candidatoRepository
                    .GetById(dto.CandidatoId);

            if (candidato == null)
            {
                throw new Exception(
                    "Candidato no encontrado.");
            }

            if (!candidato.Estado)
            {
                throw new Exception(
                    "El candidato está inactivo.");
            }

            var puesto =
                await _puestoRepository
                    .GetById(dto.PuestoElectivoId);

            if (puesto == null)
            {
                throw new Exception(
                    "Puesto electivo no encontrado.");
            }

            if (!puesto.EsActivo)
            {
                throw new Exception(
                    "El puesto electivo está inactivo.");
            }

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

            bool puestoOcupado =
                await _asignacionRepository
                    .ExisteAsignacionPorPuestoAsync(
                        dto.PuestoElectivoId,
                        partidoDirigenteId);

            if (puestoOcupado)
            {
                throw new Exception(
                    "Este puesto electivo ya tiene un candidato asignado dentro del partido.");
            }

            bool candidatoAsignado =
                await _asignacionRepository
                    .CandidatoTieneAsignacionAsync(
                        dto.CandidatoId,
                        partidoDirigenteId);

            if (candidatoAsignado)
            {
                throw new Exception(
                    "Este candidato ya está asignado a un puesto dentro del partido.");
            }

            // CANDIDATO ALIADO
            if (candidato.PartidoPoliticoId !=
                partidoDirigenteId)
            {
                bool existeAlianza =
                    await _alianzaRepository
                        .ExisteAlianzaAsync(
                            partidoDirigenteId,
                            candidato.PartidoPoliticoId);

                if (!existeAlianza)
                {
                    throw new Exception(
                        "No existe una alianza vigente con el partido de este candidato.");
                }

                var asignacionOrigen =
                    await _asignacionRepository
                        .ObtenerAsignacionOrigenAsync(
                            dto.CandidatoId);

                if (asignacionOrigen == null)
                {
                    throw new Exception(
                        "Este candidato aliado no tiene un puesto asignado en su partido de origen.");
                }

                if (asignacionOrigen
                    .PuestoElectivoId !=
                    dto.PuestoElectivoId)
                {
                    throw new Exception(
                        "Este candidato en su partido de origen aspira a un puesto diferente al seleccionado.");
                }
            }

            var asignacion =
                _mapper.Map<AsignacionCandidato>(
                    dto);

            asignacion.PartidoPoliticoId =
                partidoDirigenteId;

            await _asignacionRepository
                .AddAsync(asignacion);
        }

        public async Task EliminarAsignacionAsync(
            int asignacionId,
            int partidoDirigenteId)
        {
            if (await _eleccionRepository
                .ExisteEleccionActivaAsync())
            {
                throw new Exception(
                    "No se puede eliminar una asignación mientras exista una elección activa.");
            }

            var asignacion =
                await _asignacionRepository
                    .GetById(asignacionId);

            if (asignacion == null)
            {
                throw new Exception(
                    "La asignación seleccionada no existe o ya fue eliminada.");
            }

            bool pertenece =
                await _asignacionRepository
                    .PerteneceAlPartidoAsync(
                        asignacionId,
                        partidoDirigenteId);

            if (!pertenece)
            {
                throw new Exception(
                    "No tiene permisos para eliminar esta asignación.");
            }

            await _asignacionRepository
                .DeleteAsync(asignacionId);
        }

        public async Task<List<AsignacionCandidatoDto>>
            ObtenerPorEleccionAsync(
            int eleccionId)
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
            ObtenerPorPuestoAsync(
            int puestoId)
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