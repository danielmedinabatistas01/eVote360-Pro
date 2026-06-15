using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Repositories;

namespace eVote360Pro.Core.Application.Services
{
    public class AlianzaPoliticaService
        : GenericService<AlianzaPoliticaDto, AlianzaPolitica>,
          IAlianzaPoliticaService
    {
        private readonly IAlianzaPoliticaRepository
            _alianzaRepository;

        public AlianzaPoliticaService(
            IAlianzaPoliticaRepository alianzaRepository,
            IMapper mapper)
            : base(alianzaRepository, mapper)
        {
            _alianzaRepository = alianzaRepository;
        }

        public Task ActivarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DesactivarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AlianzaPoliticaDto>>
            GetActivosAsync()
        {
            var alianzas =
                await _alianzaRepository.GetActivosAsync();

            return _mapper.Map<List<AlianzaPoliticaDto>>
                (alianzas);
        }
    }
}