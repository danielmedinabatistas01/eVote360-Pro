using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public class CiudadanoRepository
       : GenericRepository<Ciudadano>,
         ICiudadanoRepository
    {
        private readonly ApplicationDbContext _context;

        public CiudadanoRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExisteDocumentoAsync(string documento)
        {
            return await _context.Set<Ciudadano>()
                .Where(x => x.NumeroIdentificacion == documento)
                .AnyAsync();
        }

        public async Task<bool> ExisteCorreoAsync(string correo)
        {
            return await _context.Set<Ciudadano>()
                .Where(x => x.CorreoElectronico == correo)
                .AnyAsync();
        }

        public async Task<Ciudadano?> ObtenerPorDocumentoAsync(string documento)
        {
            return await _context.Set<Ciudadano>()
                .FirstOrDefaultAsync(x => x.NumeroIdentificacion == documento);
        }
    }        
    
}
