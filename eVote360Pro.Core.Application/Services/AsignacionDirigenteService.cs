using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Core.Domain.Enums;

namespace eVote360Pro.Core.Application.Services
{
    public class AsignacionDirigenteService : IAsignacionDirigenteService
    {
        private readonly IAsignacionDirigenteRepository _asignacionRepository;
        private readonly IEleccionRepository _eleccionRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPartidoPoliticoRepository _partidoRepository;
        private readonly IMapper _mapper;

        public AsignacionDirigenteService(
            IAsignacionDirigenteRepository asignacionRepository,
            IEleccionRepository eleccionRepository,
            IUsuarioRepository usuarioRepository,
            IPartidoPoliticoRepository partidoRepository,
            IMapper mapper)
        {
            _asignacionRepository = asignacionRepository;
            _eleccionRepository = eleccionRepository;
            _usuarioRepository = usuarioRepository;
            _partidoRepository = partidoRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(AsignacionDirigenteDto dto)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se permiten acciones durante elección activa.");

            var asignaciones = await _asignacionRepository.GetAllList();
            if (asignaciones.Any(a => a.UsuarioId == dto.UsuarioId))
                throw new Exception("Este usuario ya se encuentra asignado como dirigente.");

            if (asignaciones.Any(a => a.PartidoPoliticoId == dto.PartidoPoliticoId))
                throw new Exception("Este partido ya tiene un dirigente asignado.");

            var usuario = await _usuarioRepository.GetById(dto.UsuarioId);
            if (usuario == null || !usuario.Estado || usuario.RolUsuario != RolUsuario.Dirigente)
                throw new Exception("Usuario seleccionado no es un dirigente político activo.");

            var partido = await _partidoRepository.GetById(dto.PartidoPoliticoId);
            if (partido == null || !partido.EsActivo)
                throw new Exception("Partido seleccionado no se encuentra activo.");

            var entity = _mapper.Map<AsignacionDirigente>(dto);
            await _asignacionRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, AsignacionDirigenteDto dto)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se permiten acciones durante elección activa.");

            var entity = await _asignacionRepository.GetById(id);
            if (entity == null) throw new Exception("Asignación no encontrada.");

            _mapper.Map(dto, entity);
            await _asignacionRepository.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(int id)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se permiten acciones durante elección activa.");

            var entity = await _asignacionRepository.GetById(id);
            if (entity == null) throw new Exception("Asignación no encontrada.");

            await _asignacionRepository.DeleteAsync(id);
        }

        public async Task<List<AsignacionDirigenteDto>> GetAllAsync()
        {
            var list = await _asignacionRepository.GetAllList();
            return _mapper.Map<List<AsignacionDirigenteDto>>(list);
        }

        public async Task<AsignacionDirigenteDto> GetByIdAsync(int id)
        {
            var entity = await _asignacionRepository.GetById(id);
            return _mapper.Map<AsignacionDirigenteDto>(entity);
        }
    }
}