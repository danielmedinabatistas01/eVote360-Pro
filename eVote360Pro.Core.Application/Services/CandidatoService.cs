using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;


namespace eVote360Pro.Core.Application.Services
{
    public class CandidatoService : ICandidatoService
    {
        private readonly ICandidatoRepository _repository;

        public CandidatoService(
            ICandidatoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CandidatoDto>> GetAllAsync()
        {
            var candidatos = await _repository.GetAllList();

            return candidatos.Select(x => new CandidatoDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                FotoUrl = x.FotoUrl,
                Estado = x.Estado,
                PartidoPoliticoId = x.PartidoPoliticoId
            }).ToList();
        }

        public async Task<CandidatoDto?> GetByIdAsync(int id)
        {
            var candidato = await _repository.GetById(id);

            if (candidato == null)
                return null;

            return new CandidatoDto
            {
                Id = candidato.Id,
                Nombre = candidato.Nombre,
                Apellido = candidato.Apellido,
                FotoUrl = candidato.FotoUrl,
                Estado = candidato.Estado,
                PartidoPoliticoId = candidato.PartidoPoliticoId
            };
        }

        public async Task AddAsync(CandidatoDto dto)
        {
            await _repository.AddAsync(new Candidato
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FotoUrl = dto.FotoUrl,
                Estado = dto.Estado,
                PartidoPoliticoId = dto.PartidoPoliticoId
            });
        }

        public async Task UpdateAsync(int id, CandidatoDto dto)
        {
            await _repository.UpdateAsync(id, new Candidato
            {
                Id = id,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FotoUrl = dto.FotoUrl,
                Estado = dto.Estado,
                PartidoPoliticoId = dto.PartidoPoliticoId
            });
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<CandidatoDto>> GetActivosAsync()
        {
            var candidatos = await _repository.GetActivosAsync();

            return candidatos.Select(x => new CandidatoDto
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                FotoUrl = x.FotoUrl,
                Estado = x.Estado,
                PartidoPoliticoId = x.PartidoPoliticoId
            }).ToList();
        }

        public async Task ActivarCandidatoAsync(int id)
        {
            var candidato = await _repository.GetById(id);

            if (candidato == null)
            {
                throw new Exception("Candidato no encontrado.");
            }

            candidato.Estado = true;

            await _repository.UpdateAsync(id, candidato);
        }

        public async Task DesactivarCandidatoAsync(int id)
        {
            var candidato = await _repository.GetById(id);

            if (candidato == null)
            {
                throw new Exception("Candidato no encontrado.");
            }

            candidato.Estado = false;

            await _repository.UpdateAsync(id, candidato);
        }
    }
}
