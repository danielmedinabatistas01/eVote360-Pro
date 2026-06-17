using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public class CandidatoRepository
        : GenericRepository<Candidato>,
          ICandidatoRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidatoRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public Task<bool> ExisteEleccionActivaAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Candidato>>
            GetByPartidoPoliticoAsync(
                int partidoPoliticoId)
        {
            return await _context.Candidatos
                .Where(x =>
                    x.PartidoPoliticoId ==
                    partidoPoliticoId)
                .ToListAsync();
        }

        public async Task<List<Candidato>> GetActivosAsync()
        {
            return await _context.Candidatos
                .Where(x => x.Estado)
                .ToListAsync();
        }

        public async Task<bool>HaParticipadoEnEleccionAsync(int candidatoId)
        {
            return await _context
                .AsignacionesCandidatos
                .AnyAsync(x =>
                    x.CandidatoId ==
                    candidatoId);
        }

        public async Task<bool>TieneAsignacionVigenteAsync(int candidatoId)
        {
            return await _context
                .AsignacionesCandidatos
                .AnyAsync(x =>
                    x.CandidatoId ==
                    candidatoId);
        }


    }
}