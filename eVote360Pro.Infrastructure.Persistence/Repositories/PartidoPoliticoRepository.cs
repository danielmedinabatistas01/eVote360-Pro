using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public class PartidoPoliticoRepository: GenericRepository<PartidoPolitico>,
          IPartidoPoliticoRepository
    {
        private readonly ApplicationDbContext _context;

        public PartidoPoliticoRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExisteSiglaAsync(string sigla)
        {
            return await _context.Set<PartidoPolitico>()
                .AnyAsync(x => x.Siglas == sigla);
        }

        public async Task<List<PartidoPolitico>> ObtenerActivosAsync()
        {
            return await _context.Set<PartidoPolitico>()
                .Where(x => x.EsActivo)
                .ToListAsync();
        }
}
}
