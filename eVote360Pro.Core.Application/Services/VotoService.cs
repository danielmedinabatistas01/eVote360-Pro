using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class VotoService : IVotoService
    {
        private readonly IVotoRepository _votoRepository;

        public VotoService(IVotoRepository votoRepository)
        {
            _votoRepository = votoRepository;
        }

        public async Task CrearVotoAsync(VotoDTO dto)
        {
            var entity = new Voto
            {
                EleccionId = dto.EleccionId,
                CiudadanoId = dto.CiudadanoId,
                FechaVoto = DateTime.Now,
                VotoDetalles = dto.VotoDetalles.Select(d => new VotoDetalle
                {
                    PuestoElectivoId = d.PuestoElectivoId,
                    CandidatoId = d.CandidatoId
                }).ToList()
            };

            await _votoRepository.AddAsync(entity);
        }

        public async Task<bool> CiudadanoYaVotoAsync(int ciudadanoId, int eleccionId)
        {
            return await _votoRepository.CiudadanoYaVotoAsync(ciudadanoId, eleccionId);
        }

        public async Task<int> CountCiudadanosVotaronAsync(int eleccionId)
        {
            return await _votoRepository.CountCiudadanosVotaronAsync(eleccionId);
        }

        public async Task<List<VotoDTO>> GetByEleccionIdAsync(int eleccionId)
        {
            var votos = await _votoRepository.GetByEleccionIdAsync(eleccionId);

            return votos.Select(x => new VotoDTO
            {
                Id = x.Id,
                EleccionId = x.EleccionId,
                CiudadanoId = x.CiudadanoId,
                FechaVoto = x.FechaVoto,
                VotoDetalles = x.VotoDetalles.Select(d => new VotoDetalleDTO
                {
                    Id = d.Id,
                    VotoId = d.VotoId,
                    PuestoElectivoId = d.PuestoElectivoId,
                    CandidatoId = d.CandidatoId
                }).ToList()
            }).ToList();
        }
    }
}