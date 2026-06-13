using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public class EleccionPuestoElectivoRepository : GenericRepository<EleccionPuestoElectivo>, IEleccionPuestoElectivoRepository
    {
        private readonly ApplicationDbContext _context;

        public EleccionPuestoElectivoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<EleccionPuestoElectivo>> GetByEleccionIdAsync(int eleccionId)
        {
            return await _context.EleccionPuestoElectivos
                .Where(x => x.EleccionId == eleccionId)
                .ToListAsync();
        }
    }
}