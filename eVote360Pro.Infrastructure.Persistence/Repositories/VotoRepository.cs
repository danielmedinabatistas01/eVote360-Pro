using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public class VotoRepository
        : GenericRepository<Voto>,
          IVotoRepository
    {
        private readonly ApplicationDbContext _context;

        public VotoRepository(
            ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> CiudadanoYaVotoAsync(
            int ciudadanoId,
            int eleccionId)
        {
            return await _context.Votos
                .AnyAsync(x =>
                    x.CiudadanoId == ciudadanoId &&
                    x.EleccionId == eleccionId);
        }

        public async Task<int> CountCiudadanosVotaronAsync(int eleccionId)
        {
            return await _context.Votos
                .CountAsync(x =>
                    x.EleccionId == eleccionId);
        }

        public async Task<List<Voto>>GetByEleccionIdAsync(int eleccionId)
        {
            return await _context.Votos
                .Where(x =>
                    x.EleccionId == eleccionId)
                .ToListAsync();
        }
    }
}
