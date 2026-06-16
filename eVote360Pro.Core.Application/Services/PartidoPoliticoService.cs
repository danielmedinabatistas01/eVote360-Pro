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
        private readonly IMapper _mapper;

        public PartidoPoliticoService(
            IPartidoPoliticoRepository partidoRepository,
            ICandidatoRepository candidatoRepository,
            IEleccionRepository eleccionRepository,
            IMapper mapper)
        {
            _partidoRepository = partidoRepository;
            _candidatoRepository = candidatoRepository;
            _eleccionRepository = eleccionRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(PartidoPoliticoDto dto)
        {
            
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se pueden agregar partidos políticos si existe una elección activa.");

            var entity = _mapper.Map<PartidoPolitico>(dto);
            entity.EsActivo = true; 
            await _partidoRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, PartidoPoliticoDto dto)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se pueden modificar partidos políticos si existe una elección activa.");

          
            var entity = await _partidoRepository.GetById(id);
            if (entity == null) throw new Exception("Partido político no encontrado.");

            _mapper.Map(dto, entity);
            
            await _partidoRepository.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(int id)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se pueden realizar eliminaciones si existe una elección activa.");

            
            var candidatos = await _candidatoRepository.GetAllList();
            bool tieneCandidatos = candidatos.Any(c => c.PartidoPoliticoId == id && c.Estado);
            if (tieneCandidatos)
                throw new Exception("No se puede desactivar el partido político porque contiene candidatos activos asociados.");

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
    }
}