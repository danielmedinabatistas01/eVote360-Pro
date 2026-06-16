using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public class PuestoElectivoRepository
         : GenericRepository<PuestoElectivo>,
           IPuestoElectivoRepository
    {
        private readonly ApplicationDbContext _context;

        public PuestoElectivoRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _context.Set<PuestoElectivo>()
                .AnyAsync(x => x.Nombre == nombre);
        }

        public async Task<List<PuestoElectivo>> ObtenerActivosAsync()
        {
            return await _context.Set<PuestoElectivo>()
                .Where(x => x.EsActivo)
                .ToListAsync();
        }
}
}

