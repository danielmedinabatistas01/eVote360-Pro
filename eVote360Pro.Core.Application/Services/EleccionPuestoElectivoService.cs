using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class EleccionPuestoElectivoService : IEleccionPuestoElectivoService
    {
        private readonly IEleccionPuestoElectivoRepository _repository;

        public EleccionPuestoElectivoService(
            IEleccionPuestoElectivoRepository repository)
        {
            _repository = repository;
        }

        public async Task AsignarAsync(EleccionPuestoElectivoDTO dto)
        {
            var entity = new EleccionPuestoElectivo
            {
                EleccionId = dto.EleccionId,
                PuestoElectivoId = dto.PuestoElectivoId
            };

            await _repository.AddAsync(entity);
        }

        public async Task<List<EleccionPuestoElectivoDTO>> GetByEleccionIdAsync(int eleccionId)
        {
            var entities = await _repository.GetByEleccionIdAsync(eleccionId);

            return entities.Select(x => new EleccionPuestoElectivoDTO
            {
                Id = x.Id,
                EleccionId = x.EleccionId,
                PuestoElectivoId = x.PuestoElectivoId
            }).ToList();
        }
    }
}