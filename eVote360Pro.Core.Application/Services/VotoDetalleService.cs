using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class VotoDetalleService : IVotoDetalleService
    {
        private readonly IVotoDetalleRepository _votoDetalleRepository;

        public VotoDetalleService(IVotoDetalleRepository votoDetalleRepository)
        {
            _votoDetalleRepository = votoDetalleRepository;
        }

        public async Task<List<VotoDetalleDTO>> GetByEleccionIdAsync(int eleccionId)
        {
            var detalles = await _votoDetalleRepository.GetByEleccionIdAsync(eleccionId);

            return detalles.Select(x => new VotoDetalleDTO
            {
                Id = x.Id,
                VotoId = x.VotoId,
                PuestoElectivoId = x.PuestoElectivoId,
                CandidatoId = x.CandidatoId
            }).ToList();
        }

        public async Task CreateAsync(VotoDetalleDTO dto)
        {
            var entity = new VotoDetalle
            {
                VotoId = dto.VotoId,
                PuestoElectivoId = dto.PuestoElectivoId,
                CandidatoId = dto.CandidatoId
            };

            await _votoDetalleRepository.AddAsync(entity);
        }
    }
}