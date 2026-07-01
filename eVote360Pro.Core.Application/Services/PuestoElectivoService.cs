using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Core.Domain.Enums;

namespace eVote360Pro.Core.Application.Services
{
    public class PuestoElectivoService : IPuestoElectivoService
    {
        private readonly IPuestoElectivoRepository _puestoRepository;
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IEleccionRepository _eleccionRepository;
        private readonly IMapper _mapper;

        public PuestoElectivoService(
            IPuestoElectivoRepository puestoRepository,
            ICandidatoRepository candidatoRepository,
            IEleccionRepository eleccionRepository,
            IMapper mapper)
        {
            _puestoRepository = puestoRepository;
            _candidatoRepository = candidatoRepository;
            _eleccionRepository = eleccionRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(PuestoElectivoDto dto)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se permiten acciones durante elección activa.");

            var puestos = await _puestoRepository.GetAllList();
            string nombreClean = dto.Nombre.Trim();
            if (puestos.Any(p => p.Nombre.Trim().Equals(nombreClean, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Ya existe un puesto electivo registrado con este nombre.");

            var entity = _mapper.Map<PuestoElectivo>(dto);
            entity.Nombre = nombreClean;
            entity.EsActivo = true;
            await _puestoRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, PuestoElectivoDto dto)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se permiten acciones durante elección activa.");

            var entity = await _puestoRepository.GetById(id);
            if (entity == null) throw new Exception("Puesto electivo no encontrado.");

            bool participo = elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Finalizada) || elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa);
            string nombreClean = dto.Nombre.Trim();

            if (participo && !entity.Nombre.Equals(nombreClean, StringComparison.OrdinalIgnoreCase))
                throw new Exception("No se puede modificar nombre de este puesto porque ya participó en una elección.");

            var puestos = await _puestoRepository.GetAllList();
            if (puestos.Any(p => p.Nombre.Trim().Equals(nombreClean, StringComparison.OrdinalIgnoreCase) && p.Id != id))
                throw new Exception("Ya existe otro puesto electivo registrado con este nombre.");

            _mapper.Map(dto, entity);
            entity.Nombre = nombreClean;
            await _puestoRepository.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(int id)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se permiten acciones durante elección activa.");

            var candidatos = await _candidatoRepository.GetAllList();
            if (candidatos.Any(c => c.Asignaciones != null && c.Asignaciones.Any(ac => ac.PuestoElectivoId == id) && c.Estado))
                throw new Exception("No se puede desactivar este puesto porque tiene candidatos activos asignados.");

            var entity = await _puestoRepository.GetById(id);
            if (entity == null) throw new Exception("Puesto electivo no encontrado.");

            entity.EsActivo = false;
            await _puestoRepository.UpdateAsync(id, entity);
        }

        public async Task<List<PuestoElectivoDto>> GetAllAsync()
        {
            var list = await _puestoRepository.GetAllList();
            return _mapper.Map<List<PuestoElectivoDto>>(list);
        }

        public async Task<PuestoElectivoDto> GetByIdAsync(int id)
        {
            var entity = await _puestoRepository.GetById(id);
            return _mapper.Map<PuestoElectivoDto>(entity);
        }

        public async Task ActivarAsync(int id)
        {
            var entity = await _puestoRepository.GetById(id);

            if (entity == null)
                throw new Exception("Puesto no encontrado.");

            entity.EsActivo = true;

            await _puestoRepository.UpdateAsync(id, entity);
        }
    }
}