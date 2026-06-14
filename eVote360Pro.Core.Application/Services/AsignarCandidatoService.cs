using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Services
{
    public class AsignacionCandidatoService : IAsignacionCandidatoService
    {
        private readonly IAsignacionCandidatoRepository _repository;

        public AsignacionCandidatoService(
            IAsignacionCandidatoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AsignacionCandidatoDto>> GetAllAsync()
        {
            var asignaciones = await _repository.GetAllList();

            return asignaciones.Select(x => new AsignacionCandidatoDto
            {
                Id = x.Id,
                CandidatoId = x.CandidatoId,
                PuestoElectivoId = x.PuestoElectivoId,
                EleccionId = x.EleccionId


            }).ToList();
        }

        public async Task<AsignacionCandidatoDto?> GetByIdAsync(int id)
        {
            var candidato = await _repository.GetById(id);

            if (candidato == null)
                return null;

            return new AsignacionCandidatoDto
            {
                Id = candidato.Id,
                CandidatoId = candidato.CandidatoId,
                PuestoElectivoId = candidato.PuestoElectivoId,
                EleccionId = candidato.EleccionId
            };
        }

        public async Task AddAsync(AsignacionCandidatoDto dto)
        {
            await _repository.AddAsync(new AsignacionCandidato
            {
                Id = dto.Id,
                CandidatoId = dto.CandidatoId,
                PuestoElectivoId = dto.PuestoElectivoId,
                EleccionId = dto    .EleccionId
            });
        }

        public async Task UpdateAsync(int id, AsignacionCandidatoDto dto)
        {
            await _repository.UpdateAsync(id, new AsignacionCandidato
            {
                Id = id,
                CandidatoId = dto.CandidatoId,
                PuestoElectivoId = dto.PuestoElectivoId,
                EleccionId = dto.EleccionId
            });
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> ExisteAsignacionAsync(
    int candidatoId,
    int puestoId,
    int eleccionId)
        {
            return await _repository
                .ExisteAsignacionAsync(
                    candidatoId,
                    puestoId,
                    eleccionId);
        }

        public async Task AsignarCandidatoAsync(
    AsignacionCandidatoDto dto)
        {
            bool existe =
                await ExisteAsignacionAsync(
                    dto.CandidatoId,
                    dto.PuestoElectivoId,
                    dto.EleccionId);

            if (existe)
            {
                throw new Exception(
                    "El candidato ya está asignado.");
            }

            await _repository.AddAsync(
                new AsignacionCandidato
                {
                    CandidatoId = dto.CandidatoId,
                    PuestoElectivoId = dto.PuestoElectivoId,
                    EleccionId = dto.EleccionId
                });
        }

        public async Task<List<AsignacionCandidatoDto>>
    ObtenerPorEleccionAsync(int eleccionId)
        {
            var asignaciones =
                await _repository
                    .ObtenerPorEleccionAsync(eleccionId);

            return asignaciones.Select(x =>
                new AsignacionCandidatoDto
                {
                    Id = x.Id,
                    CandidatoId = x.CandidatoId,
                    PuestoElectivoId = x.PuestoElectivoId,
                    EleccionId = x.EleccionId
                }).ToList();
        }


        public async Task<List<AsignacionCandidatoDto>>
    ObtenerPorPuestoAsync(int puestoId)
        {
            var asignaciones =
                await _repository
                    .ObtenerPorPuestoAsync(puestoId);

            return asignaciones.Select(x =>
                new AsignacionCandidatoDto
                {
                    Id = x.Id,
                    CandidatoId = x.CandidatoId,
                    PuestoElectivoId = x.PuestoElectivoId,
                    EleccionId = x.EleccionId
                }).ToList();
        }
    }
}
