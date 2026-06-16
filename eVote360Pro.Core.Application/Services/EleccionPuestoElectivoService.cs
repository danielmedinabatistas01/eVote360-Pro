using AutoMapper;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class EleccionPuestoElectivoService
        : GenericService<EleccionPuestoElectivoDTO, EleccionPuestoElectivo>,
          IEleccionPuestoElectivoService
    {
        private readonly IEleccionPuestoElectivoRepository _repository;

        public EleccionPuestoElectivoService(
            IEleccionPuestoElectivoRepository repository,
            IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
        }

        public async Task AsignarAsync(EleccionPuestoElectivoDTO dto)
        {
            await base.AddAsync(dto);
        }

        public async Task<List<EleccionPuestoElectivoDTO>> GetByEleccionIdAsync(int eleccionId)
        {
            var entities = await _repository.GetByEleccionIdAsync(eleccionId);

            return _mapper.Map<List<EleccionPuestoElectivoDTO>>(entities);
        }
    }
}