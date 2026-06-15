using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class VotoService : IVotoService
    {
        private readonly IVotoRepository _repository;
        private readonly IVotoRepository _votoRepository;

        public VotoService(
            IVotoRepository repository)
        public VotoService(IVotoRepository votoRepository)
        {
            _repository = repository;
            _votoRepository = votoRepository;
        }

        public async Task<List<VotoDto>> GetAllAsync()
        public async Task CrearVotoAsync(VotoDTO dto)
        {
            var votos = await _repository.GetAllList();

            return votos.Select(x => new VotoDto
            var entity = new Voto
            {
                Id = x.Id,
                CiudadanoId = x.CiudadanoId,
                EleccionId = x.EleccionId,
                FechaVotacion = x.FechaVotacion

            }).ToList();
        }

        public async Task<VotoDto?> GetByIdAsync(int id)
                EleccionId = dto.EleccionId,
                CiudadanoId = dto.CiudadanoId,
                FechaVoto = DateTime.Now,
                VotoDetalles = dto.VotoDetalles.Select(d => new VotoDetalle
        {
            var voto = await _repository.GetById(id);

            if (voto == null)
                return null;

            return new VotoDto
            {
                Id = voto.Id,
                CiudadanoId = voto.CiudadanoId,
                EleccionId = voto.EleccionId,
                FechaVotacion = voto.FechaVotacion
                    PuestoElectivoId = d.PuestoElectivoId,
                    CandidatoId = d.CandidatoId
                }).ToList()
            };
        }

        public async Task AddAsync(VotoDto dto)
        {
            await _repository.AddAsync(new Voto
            {
                CiudadanoId = dto.CiudadanoId,
                EleccionId = dto.EleccionId,
                FechaVotacion = dto.FechaVotacion
            });
            await _votoRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(int id, VotoDto dto)
        public async Task<bool> CiudadanoYaVotoAsync(int ciudadanoId, int eleccionId)
        {
            await _repository.UpdateAsync(id, new Voto
            {
                CiudadanoId = dto.CiudadanoId,
                EleccionId = dto.EleccionId,
                FechaVotacion = dto.FechaVotacion
            });
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }


        public async Task<bool>CiudadanoYaVotoAsync(int ciudadanoId,int eleccionId)
        {
            return await _repository
                .CiudadanoYaVotoAsync(
                    ciudadanoId,
                    eleccionId);
            return await _votoRepository.CiudadanoYaVotoAsync(ciudadanoId, eleccionId);
        }

        public async Task<bool>PuedeVotarAsync(int ciudadanoId,int eleccionId)
        public async Task<int> CountCiudadanosVotaronAsync(int eleccionId)
        {
            bool yaVoto =
                await _repository
                    .CiudadanoYaVotoAsync(
                        ciudadanoId,
                        eleccionId);

            return !yaVoto;
            return await _votoRepository.CountCiudadanosVotaronAsync(eleccionId);
        }

        public async Task RegistrarVotoAsync(VotoDto dto)
        public async Task<List<VotoDTO>> GetByEleccionIdAsync(int eleccionId)
        {
            bool yaVoto =
                await _repository
                    .CiudadanoYaVotoAsync(
                        dto.CiudadanoId,
                        dto.EleccionId);
            var votos = await _votoRepository.GetByEleccionIdAsync(eleccionId);

            if (yaVoto)
            return votos.Select(x => new VotoDTO
            {
                throw new Exception(
                    "El ciudadano ya votó.");
            }

            await _repository.AddAsync(
                new Voto
                Id = x.Id,
                EleccionId = x.EleccionId,
                CiudadanoId = x.CiudadanoId,
                FechaVoto = x.FechaVoto,
                VotoDetalles = x.VotoDetalles.Select(d => new VotoDetalleDTO
                {
                    CiudadanoId = dto.CiudadanoId,
                    EleccionId = dto.EleccionId,
                    FechaVotacion = DateTime.Now
                });
                    Id = d.Id,
                    VotoId = d.VotoId,
                    PuestoElectivoId = d.PuestoElectivoId,
                    CandidatoId = d.CandidatoId
                }).ToList()
            }).ToList();
        }

    }
}
