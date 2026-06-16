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
        private readonly IEleccionRepository _eleccionRepository;
        private readonly IMapper _mapper;

        public PuestoElectivoService(
            IPuestoElectivoRepository puestoRepository,
            IEleccionRepository eleccionRepository,
            IMapper mapper)
        {
            _puestoRepository = puestoRepository;
            _eleccionRepository = eleccionRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(PuestoElectivoDto dto)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se pueden agregar puestos electivos si existe una elección activa.");

            var entity = _mapper.Map<PuestoElectivo>(dto);
            entity.EsActivo = true;
            await _puestoRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, PuestoElectivoDto dto)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se pueden modificar puestos electivos si existe una elección activa.");

            var entity = await _puestoRepository.GetById(id);
            if (entity == null) throw new Exception("Puesto electivo no encontrado.");

            _mapper.Map(dto, entity);
            await _puestoRepository.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(int id)
        {
            var elecciones = await _eleccionRepository.GetAllList();
            if (elecciones.Any(e => e.EstadoEleccion == EstadoEleccion.Activa))
                throw new Exception("No se pueden eliminar o desactivar puestos electivos si existe una elección activa.");

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
    }
}