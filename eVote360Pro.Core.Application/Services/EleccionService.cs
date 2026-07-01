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
        private readonly IPuestoElectivoRepository _puestoRepository;
        private readonly IPartidoPoliticoRepository _partidoRepository;
        private readonly IEleccionPuestoElectivoRepository _eleccionPuestoRepository;

        public EleccionService(
            IEleccionRepository eleccionRepository,
            IAsignacionCandidatoRepository asignacionCandidatoRepository,
            IPuestoElectivoRepository puestoRepository,
            IPartidoPoliticoRepository partidoRepository,
            IEleccionPuestoElectivoRepository eleccionPuestoRepository,
            IMapper mapper)
            : base(eleccionRepository, mapper)
        {
            _eleccionRepository = eleccionRepository;
            _asignacionCandidatoRepository = asignacionCandidatoRepository;
            _puestoRepository = puestoRepository;
            _partidoRepository = partidoRepository;
            _eleccionPuestoRepository = eleccionPuestoRepository;
        }

        public async Task<List<EleccionIndexViewModel>> GetAllAsync()
        {
            var elecciones = await _eleccionRepository.GetAllOrdenadasAsync();

            return elecciones.Select(x => new EleccionIndexViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                FechaRealizacion = x.FechaRealizacion,
                EstadoEleccion = x.EstadoEleccion
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
                throw new Exception("La elección no existe.");

            if (eleccion.EstadoEleccion != EstadoEleccion.Pendiente)
                throw new Exception("Solo se pueden activar elecciones pendientes.");

            if (await _eleccionRepository.ExisteEleccionActivaAsync())
                throw new Exception("Ya existe una elección activa.");

            var puestos = await _puestoRepository.ObtenerActivosAsync();

            if (!puestos.Any())
                throw new Exception("Debe existir al menos un puesto electivo activo.");


            var partidos = (await _partidoRepository.GetAllList())
                .Where(x => x.EsActivo)
                .ToList();

            if (partidos.Count < 2)
                throw new Exception("Debe haber al menos dos partidos políticos activos.");


            var asignaciones = await _asignacionCandidatoRepository
                .ObtenerPorEleccionAsync(eleccion.Id);

            if (!asignaciones.Any())
                throw new Exception("No existen candidatos asignados para esta elección.");

            foreach (var partido in partidos)
            {
                foreach (var puesto in puestos)
                {
                    bool existeAsignacion = asignaciones.Any(x =>
                        x.PartidoPoliticoId == partido.Id &&
                        x.PuestoElectivoId == puesto.Id &&
                        x.Candidato != null &&
                        x.Candidato.Estado);

                    if (!existeAsignacion)
                    {
                        throw new Exception(
                            $"El partido '{partido.Nombre}' no tiene un candidato activo asignado para el puesto '{puesto.Nombre}'.");
                    }
                }
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

            var entity = _mapper.Map<Eleccion>(dto);

            var eleccion = await _eleccionRepository.AddAsync(entity);

            if (eleccion == null)
                throw new Exception("No fue posible crear la elección.");

            var puestos = await _puestoRepository.ObtenerActivosAsync();

            foreach (var puesto in puestos)
            {
                await _eleccionPuestoRepository.AddAsync(
                    new EleccionPuestoElectivo
                    {
                        EleccionId = eleccion.Id,
                        PuestoElectivoId = puesto.Id
                    });
            }
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