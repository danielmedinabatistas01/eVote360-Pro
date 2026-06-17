using AutoMapper;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.Eleccion;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Enums;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class EleccionService
        : GenericService<EleccionDTO, Eleccion>,
          IEleccionService
    {
        private readonly IEleccionRepository _eleccionRepository;
        private readonly IAsignacionCandidatoRepository _asignacionCandidatoRepository;

        public EleccionService(
       IEleccionRepository eleccionRepository,
       IAsignacionCandidatoRepository asignacionCandidatoRepository,
       IMapper mapper)
       : base(eleccionRepository, mapper)
        {
            _eleccionRepository = eleccionRepository;
            _asignacionCandidatoRepository = asignacionCandidatoRepository;
        }

        public async Task<List<EleccionIndexViewModel>> GetAllAsync()
        {
            var elecciones = await _eleccionRepository.GetAllOrdenadasAsync();

            return elecciones.Select(x => new EleccionIndexViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                FechaRealizacion = x.FechaRealizacion,
                EstadoEleccion = x.EstadoEleccion,
            }).ToList();
        }

        public async Task<EleccionEditViewModel?> GetEditViewModelByIdAsync(int id)
        {
            var eleccion = await _eleccionRepository.GetById(id);

            if (eleccion == null)
                return null;

            return new EleccionEditViewModel
            {
                Id = eleccion.Id,
                Nombre = eleccion.Nombre,
                FechaRealizacion = eleccion.FechaRealizacion,
                EstadoEleccion = eleccion.EstadoEleccion
            };
        }

        public async Task<EleccionActivarViewModel?> GetActivarViewModelAsync(int id)
        {
            var eleccion = await _eleccionRepository.GetById(id);

            if (eleccion == null)
                return null;

            return new EleccionActivarViewModel
            {
                Id = eleccion.Id,
                Nombre = eleccion.Nombre,
                FechaRealizacion = eleccion.FechaRealizacion
            };
        }

        public async Task<EleccionFinalizarViewModel?> GetFinalizarViewModelAsync(int id)
        {
            var eleccion = await _eleccionRepository.GetById(id);

            if (eleccion == null)
                return null;

            return new EleccionFinalizarViewModel
            {
                Id = eleccion.Id,
                Nombre = eleccion.Nombre,
                FechaRealizacion = eleccion.FechaRealizacion
            };
        }

        public async Task ActivarAsync(int id)
        {
            var eleccion = await _eleccionRepository.GetByIdWithPuestosAsync(id);

            if (eleccion == null)
                throw new Exception("La elección no existe.");

            if (eleccion.EstadoEleccion != EstadoEleccion.Pendiente)
                throw new Exception("Solo se pueden activar elecciones pendientes.");

            var existeActiva = await _eleccionRepository.ExisteEleccionActivaAsync();

            if (existeActiva)
                throw new Exception("Ya existe una elección activa.");

            if (eleccion.PuestosElectivos == null || !eleccion.PuestosElectivos.Any())
                throw new Exception("La elección debe tener puestos electivos asignados antes de activarse.");

            var asignaciones = await _asignacionCandidatoRepository.ObtenerPorEleccionAsync(eleccion.Id);

            if (!asignaciones.Any())
                throw new Exception("La elección debe tener candidatos asignados antes de activarse.");

            foreach (var puesto in eleccion.PuestosElectivos)
            {
                bool tieneCandidato = asignaciones.Any(x =>
                    x.PuestoElectivoId == puesto.PuestoElectivoId);

                if (!tieneCandidato)
                    throw new Exception("Todos los puestos electivos deben tener al menos un candidato asignado.");
            }

            eleccion.EstadoEleccion = EstadoEleccion.Activa;

            await _eleccionRepository.UpdateAsync(eleccion.Id, eleccion);
        }


        public async Task FinalizarAsync(int id)
        {
            var eleccion = await _eleccionRepository.GetById(id);

            if (eleccion == null)
                throw new Exception("La elección no existe.");

            if (eleccion.EstadoEleccion != EstadoEleccion.Activa)
                throw new Exception("Solo se pueden finalizar elecciones activas.");

            eleccion.EstadoEleccion = EstadoEleccion.Finalizada;

            await _eleccionRepository.UpdateAsync(eleccion.Id, eleccion);
        }

        public override async Task AddAsync(EleccionDTO dto)
        {
            dto.Nombre = dto.Nombre.Trim();
            await base.AddAsync(dto);
        }

        public override async Task UpdateAsync(int id, EleccionDTO dto)
        {
            dto.Nombre = dto.Nombre.Trim();
            await base.UpdateAsync(id, dto);
        }

        public async Task<bool> ExisteEleccionActivaAsync()
        {
            return await _eleccionRepository.ExisteEleccionActivaAsync();
        }

        public async Task<EleccionDTO?> GetEleccionActivaAsync()
        {
            var eleccion = await _eleccionRepository.GetEleccionActivaAsync();

            if (eleccion == null)
                return null;

            return _mapper.Map<EleccionDTO>(eleccion);
        }
    }
}