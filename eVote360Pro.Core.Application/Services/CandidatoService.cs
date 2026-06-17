using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class CandidatoService
        : GenericService<CandidatoDto, Candidato>,
          ICandidatoService
    {
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IEleccionRepository _eleccionRepository;
        private readonly IAsignacionCandidatoRepository _asignacionRepository;

        public CandidatoService(
            ICandidatoRepository candidatoRepository,
            IEleccionRepository eleccionRepository,
            IAsignacionCandidatoRepository asignacionRepository,
            IMapper mapper)
            : base(candidatoRepository, mapper)
        {
            _candidatoRepository =
                candidatoRepository;

            _eleccionRepository =
                eleccionRepository;

            _asignacionRepository =
                asignacionRepository;
        }
        public async Task<List<CandidatoDto>> GetActivosAsync()
        {
            var candidatos =
                await _candidatoRepository.GetActivosAsync();

            return _mapper.Map<List<CandidatoDto>>(candidatos);
        }

        public async Task ActivarCandidatoAsync(int id)
        {
            var candidato =
                await _candidatoRepository.GetById(id);

            if (candidato == null)
            {
                throw new Exception(
                    "Candidato no encontrado.");
            }

            if (candidato.Estado)
            {
                throw new Exception(
                    "Este candidato ya se encuentra activo.");
            }

            if (await _eleccionRepository
                .ExisteEleccionActivaAsync())
            {
                throw new Exception(
                    "No se puede activar un candidato mientras exista una elección activa.");
            }

            candidato.Estado = true;

            await _candidatoRepository.UpdateAsync(id, candidato);
        }

        public async Task DesactivarCandidatoAsync(int id)
        {
            var candidato =
                await _candidatoRepository.GetById(id);

            if (candidato == null)
            {
                throw new Exception(
                    "Candidato no encontrado.");
            }

            if (!candidato.Estado)
            {
                throw new Exception(
                    "Este candidato ya se encuentra inactivo.");
            }

            if (await _eleccionRepository
                .ExisteEleccionActivaAsync())
            {
                throw new Exception(
                    "No se puede desactivar un candidato mientras exista una elección activa.");
            }

            bool asignado =
                await _asignacionRepository
                    .TieneAsignacionVigenteAsync(id);

            if (asignado)
            {
                throw new Exception(
                    "No se puede desactivar este candidato porque está asignado a un puesto electivo.");
            }

            candidato.Estado = false;

            await _candidatoRepository.UpdateAsync(id, candidato);
        }

        public override async Task AddAsync(
    CandidatoDto dto)
        {


            if (string.IsNullOrWhiteSpace(
                dto.Nombre))
            {
                throw new Exception(
                    "El nombre del candidato es requerido.");
            }

            if (string.IsNullOrWhiteSpace(
                dto.Apellido))
            {
                throw new Exception(
                    "El apellido del candidato es requerido.");
            }

            if (dto.FotoUrl == null)
            {
                throw new Exception(
                    "La foto del candidato es requerida.");
            }

            bool existeEleccionActiva =
                await _eleccionRepository
                    .ExisteEleccionActivaAsync();

            if (existeEleccionActiva)
            {
                throw new Exception(
                    "No se puede crear un candidato mientras exista una elección activa.");
            }

            await base.AddAsync(dto);
        }


        public override async Task UpdateAsync(
    int id,
    CandidatoDto dto)
        {
            var candidato =
                await _candidatoRepository
                    .GetById(id);

            if (candidato == null)
            {
                throw new Exception(
                    "Candidato no encontrado.");
            }

            bool existeEleccionActiva =
                await _eleccionRepository
                    .ExisteEleccionActivaAsync();

            if (existeEleccionActiva)
            {
                throw new Exception(
                    "No se puede editar un candidato mientras exista una elección activa.");
            }

            if (candidato.PartidoPoliticoId !=
                usuario.PartidoPoliticoId)
            {
                throw new Exception(
                    "No puede modificar candidatos de otros partidos.");
            }

            bool participo =
                await _candidatoRepository
                    .HaParticipadoEnEleccionAsync(id);

            if (participo)
            {
                throw new Exception(
                    "No se pueden modificar los datos principales de este candidato porque ya participó en una elección.");
            }

            await base.UpdateAsync(
                id,
                dto);
        }




    }
}