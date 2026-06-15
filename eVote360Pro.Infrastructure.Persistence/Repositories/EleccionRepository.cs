using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Enums;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public class EleccionRepository : GenericRepository<Eleccion>, IEleccionRepository
    {
        private readonly ApplicationDbContext _context;

        public EleccionRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExisteEleccionActivaAsync()
        {
            return await _context.Elecciones
                .AnyAsync(x => x.EstadoEleccion == EstadoEleccion.Activa);
        }

        public async Task<Eleccion?> GetEleccionActivaAsync()
        {
            return await _context.Elecciones
                .FirstOrDefaultAsync(x => x.EstadoEleccion == EstadoEleccion.Activa);
        }

        public async Task<List<Eleccion>> GetAllOrdenadasAsync()
        {
            return await _context.Elecciones
                .OrderByDescending(x => x.FechaRealizacion)
                .ToListAsync();
        }
    }
}