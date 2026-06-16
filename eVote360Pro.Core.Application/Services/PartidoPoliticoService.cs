using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class PartidoPoliticoService
        : GenericService<PartidoPoliticoDto, PartidoPolitico>,
          IPartidoPoliticoService
    {
        private readonly IPartidoPoliticoRepository _repository;

        public PartidoPoliticoService(
            IPartidoPoliticoRepository repository,
            IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
        }

        public override async Task AddAsync(
            PartidoPoliticoDto dto)
        {
            if (await _repository.ExisteSiglaAsync(dto.Siglas))
                throw new Exception("Las siglas ya existen.");

            await base.AddAsync(dto);
        }
    }
}