using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class CandidatoService
        : GenericService<CandidatoDto, Candidato>,
          ICandidatoService
    {
        private readonly ICandidatoRepository _candidatoRepository;

        public CandidatoService(
            ICandidatoRepository candidatoRepository,
            IMapper mapper)
            : base(candidatoRepository, mapper)
        {
            _candidatoRepository = candidatoRepository;
        }

        public async Task<List<CandidatoDto>> GetActivosAsync()
        {
            var candidatos =
                await _candidatoRepository.GetActivosAsync();

            return _mapper.Map<List<CandidatoDto>>(candidatos);
        }

        public async Task ActivarCandidatoAsync(int id)
        {
            var candidato =
                await _candidatoRepository.GetById(id);

            if (candidato == null)
                throw new Exception("Candidato no encontrado.");

            candidato.Estado = true;

            await _candidatoRepository.UpdateAsync(id, candidato);
        }

        public async Task DesactivarCandidatoAsync(int id)
        {
            var candidato =
                await _candidatoRepository.GetById(id);

            if (candidato == null)
                throw new Exception("Candidato no encontrado.");

            candidato.Estado = false;

            await _candidatoRepository.UpdateAsync(id, candidato);
        }
    }
}