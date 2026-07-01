using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Core.Domain.Enums;

namespace eVote360Pro.Core.Application.Services
{
    public class CiudadanoService : ICiudadanoService
    {
        private readonly ICiudadanoRepository _ciudadanoRepository;
        private readonly IVotoRepository _votoRepository;
        private readonly IEleccionRepository _eleccionRepository;
        private readonly IMapper _mapper;

        public CiudadanoService(
            ICiudadanoRepository ciudadanoRepository,
            IVotoRepository votoRepository,
            IEleccionRepository eleccionRepository,
            IMapper mapper)
        {
            _ciudadanoRepository = ciudadanoRepository;
            _votoRepository = votoRepository;
            _eleccionRepository = eleccionRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CiudadanoDto dto)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se permiten acciones durante elección activa.");

            var ciudadanos = await _ciudadanoRepository.GetAllList();

            if (ciudadanos.Any(c => c.NumeroIdentificacion == dto.NumeroIdentificacion))
                throw new Exception("Ya existe un ciudadano registrado con este número de documento.");

            var entity = _mapper.Map<Ciudadano>(dto);
            entity.NumeroIdentificacion = dto.NumeroIdentificacion;
            entity.EsActivo = true;
            await _ciudadanoRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, CiudadanoDto dto)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se permiten acciones durante elección activa.");

            var entity = await _ciudadanoRepository.GetById(id);
            if (entity == null) throw new Exception("Ciudadano no encontrado.");

            var votos = await _votoRepository.GetAllList();
            bool yaVoto = votos.Any(v => v.CiudadanoId == id);
            string docClean = dto.NumeroIdentificacion.Trim();

            if (yaVoto && entity.NumeroIdentificacion != docClean)
                throw new Exception("No se puede modificar número de documento porque ya participó en una elección.");

            var ciudadanos = await _ciudadanoRepository.GetAllList();
            if (ciudadanos.Any(c => c.NumeroIdentificacion.Trim() == docClean && c.Id != id))
                throw new Exception("El número de documento ya se encuentra asignado a otra persona.");

            _mapper.Map(dto, entity);
            entity.NumeroIdentificacion = docClean;
            await _ciudadanoRepository.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(int id)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se permiten acciones durante elección activa.");

            var entity = await _ciudadanoRepository.GetById(id);
            if (entity == null) throw new Exception("Ciudadano no encontrado.");

            entity.EsActivo = false;
            await _ciudadanoRepository.UpdateAsync(id, entity);
        }

        public async Task<List<CiudadanoDto>> GetAllAsync()
        {
            var list = await _ciudadanoRepository.GetAllList();
            return _mapper.Map<List<CiudadanoDto>>(list);
        }

        public async Task<CiudadanoDto> GetByIdAsync(int id)
        {
            var entity = await _ciudadanoRepository.GetById(id);
            return _mapper.Map<CiudadanoDto>(entity);
        }

        public async Task ActivarAsync(int id)
        {
            var entity = await _ciudadanoRepository.GetById(id);

            if (entity == null)
                throw new Exception("Ciudadano no encontrado.");

            entity.EsActivo = true;

            await _ciudadanoRepository.UpdateAsync(id, entity);
        }
    }
}