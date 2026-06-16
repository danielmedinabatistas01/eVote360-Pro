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

        public EleccionService(
            IEleccionRepository eleccionRepository,
            IMapper mapper)
            : base(eleccionRepository, mapper)
        {
            _eleccionRepository = eleccionRepository;
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
            var eleccion = await _eleccionRepository.GetById(id);

            if (eleccion == null)
                return;

            var existeActiva = await _eleccionRepository.ExisteEleccionActivaAsync();

            if (existeActiva)
                return;

            eleccion.EstadoEleccion = EstadoEleccion.Activa;

            await _eleccionRepository.UpdateAsync(eleccion.Id, eleccion);
        }

        public async Task FinalizarAsync(int id)
        {
            var eleccion = await _eleccionRepository.GetById(id);

            if (eleccion == null)
                return;

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