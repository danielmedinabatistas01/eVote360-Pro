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

        public async Task<List<AlianzaPoliticaDto>>
            GetActivosAsync()
        {
            var alianzas =
                await _alianzaRepository.GetActivosAsync();

            return _mapper.Map<List<AlianzaPoliticaDto>>
                (alianzas);
        }

        public async Task ActivarAsync(int id)
        {
            var alianza =
                await _alianzaRepository.GetById(id);

            if (alianza == null)
                throw new Exception(
                    "Alianza política no encontrada.");

            alianza.Estado = true;

            await _alianzaRepository
                .UpdateAsync(id, alianza);
        }

        public async Task DesactivarAsync(int id)
        {
            var alianza =
                await _alianzaRepository.GetById(id);

            if (alianza == null)
                throw new Exception(
                    "Alianza política no encontrada.");

            alianza.Estado = false;

            await _alianzaRepository
                .UpdateAsync(id, alianza);
        }
    }
}