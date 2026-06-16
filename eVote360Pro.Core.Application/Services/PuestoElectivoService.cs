using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class PuestoElectivoService
        : GenericService<PuestoElectivoDto, PuestoElectivo>,
          IPuestoElectivoService
    {
        private readonly IPuestoElectivoRepository _repository;

        public PuestoElectivoService(
            IPuestoElectivoRepository repository,
            IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
        }

        public override async Task AddAsync(
            PuestoElectivoDto dto)
        {
            if (await _repository.ExisteNombreAsync(dto.Nombre))
                throw new Exception("El puesto ya existe.");

            await base.AddAsync(dto);
        }
    }
}