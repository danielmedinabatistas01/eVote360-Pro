using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public class VotoDetalleRepository : GenericRepository<VotoDetalle>, IVotoDetalleRepository
    {
        private readonly ApplicationDbContext _context;

        public VotoDetalleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<VotoDetalle>> GetByEleccionIdAsync(int eleccionId)
        {
            return await _context.VotoDetalles
                .Include(x => x.Voto)
                .Where(x => x.Voto.EleccionId == eleccionId)
                .ToListAsync();
        }
    }
}