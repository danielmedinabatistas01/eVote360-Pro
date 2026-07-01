using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Core.Domain.Enums;

namespace eVote360Pro.Core.Application.Services
{
    public class PartidoPoliticoService : IPartidoPoliticoService
    {
        private readonly IPartidoPoliticoRepository _partidoRepository;
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IEleccionRepository _eleccionRepository;
        private readonly IAsignacionDirigenteRepository _dirigenteRepository;
        private readonly IAsignacionCandidatoRepository _asignacionCandidatoRepository;
        private readonly IMapper _mapper;

        public PartidoPoliticoService(
            IPartidoPoliticoRepository partidoRepository,
            ICandidatoRepository candidatoRepository,
            IEleccionRepository eleccionRepository,
            IAsignacionDirigenteRepository dirigenteRepository,
            IAsignacionCandidatoRepository asignacionCandidatoRepository,
            IMapper mapper)
        {
            _partidoRepository = partidoRepository;
            _candidatoRepository = candidatoRepository;
            _eleccionRepository = eleccionRepository;
            _dirigenteRepository = dirigenteRepository;
            _asignacionCandidatoRepository = asignacionCandidatoRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(PartidoPoliticoDto dto)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se permiten acciones durante elección activa.");

            var partidos = await _partidoRepository.GetAllList();
            
            if (partidos.Any(p => p.Nombre.ToLower() == dto.Nombre.ToLower()))
                throw new Exception("Ya existe partido registrado con este nombre.");

            if (partidos.Any(p => p.Siglas.ToUpper() == dto.Siglas.ToUpper()))
                throw new Exception("Ya existe partido registrado con estas siglas.");        

            var entity = _mapper.Map<PartidoPolitico>(dto);
                entity.EsActivo = true;
                entity.Descripcion = dto.Descripcion;

                await _partidoRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, PartidoPoliticoDto dto)
        {
            var elecciones = await _eleccionRepository.GetAllList();

            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception(
                    "No se permiten acciones durante elección activa.");

            var entity =
                await _partidoRepository.GetById(id);

            if (entity == null)
                throw new Exception(
                    "Partido político no encontrado.");

            var asignaciones = await _asignacionCandidatoRepository.GetAllList();
            bool participo = asignaciones.Any(ac => ac.PartidoPoliticoId == id && 
                (ac.Eleccion?.EstadoEleccion == EstadoEleccion.Activa || ac.Eleccion?.EstadoEleccion == EstadoEleccion.Finalizada));

            string nombreClean =
                dto.Nombre.Trim();

            string siglasClean =
                dto.Siglas.Trim().ToUpper();

            if (participo)
            {
                if (!entity.Nombre.Equals(
                        nombreClean,
                        StringComparison.OrdinalIgnoreCase)
                    ||
                    entity.Siglas != siglasClean
                    ||
                    entity.LogoUrl != dto.LogoUrl)
                {
                    throw new Exception(
                        "No se permiten modificar siglas, nombre o logo si participó en elecciones.");
                }
            }

            entity.Nombre = nombreClean;
            entity.Siglas = siglasClean;
            entity.Descripcion = dto.Descripcion;
            entity.LogoUrl = dto.LogoUrl;

            await _partidoRepository.UpdateAsync(
                id,
                entity);
        }

        public async Task DeleteAsync(int id)
            {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se permiten acciones durante elección activa.");

            var candidatos = await _candidatoRepository.GetAllList();
            if (candidatos.Any(c => c.PartidoPoliticoId == id && c.Estado))
                throw new Exception("No se puede desactivar partido político porque tiene candidatos activos.");

            var dirigentes = await _dirigenteRepository.GetAllList();
            if (dirigentes.Any(d => d.PartidoPoliticoId == id))
                throw new Exception("No se puede desactivar partido político porque tiene un dirigente asignado.");

            var entity = await _partidoRepository.GetById(id);
            if (entity == null) throw new Exception("Partido político no encontrado.");

            entity.EsActivo = false;
            await _partidoRepository.UpdateAsync(id, entity);
            }

        public async Task<List<PartidoPoliticoDto>> GetAllAsync()
        {
            var list = await _partidoRepository.GetAllList();
            return _mapper.Map<List<PartidoPoliticoDto>>(list);
        }

        public async Task<PartidoPoliticoDto> GetByIdAsync(int id)
        {
            var entity = await _partidoRepository.GetById(id);
            return _mapper.Map<PartidoPoliticoDto>(entity);
        }

        public async Task ActivarAsync(int id)
        {
            var entity = await _partidoRepository.GetById(id);

            if (entity == null)
                throw new Exception("Partido no encontrado.");

            entity.EsActivo = true;

            await _partidoRepository.UpdateAsync(id, entity);
        }
    }
}