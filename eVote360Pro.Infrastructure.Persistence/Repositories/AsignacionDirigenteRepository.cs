using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public class AsignacionDirigenteRepository
        : GenericRepository<AsignacionDirigente>,
          IAsignacionDirigenteRepository
    {
        private readonly ApplicationDbContext _context;

        public AsignacionDirigenteRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExisteAsignacionAsync(
            int usuarioId,
            int partidoId)
        {
            return await _context.Set<AsignacionDirigente>()
                .AnyAsync(x =>
                    x.UsuarioId == usuarioId &&
                    x.PartidoPoliticoId == partidoId);
        }

        public async Task<bool> PartidoTieneDirigenteAsync(
            int partidoId)
        {
            return await _context.Set<AsignacionDirigente>()
                .AnyAsync(x =>
                    x.PartidoPoliticoId == partidoId);
        }

        public async Task<bool> UsuarioTienePartidoAsync(
            int usuarioId)
        {
            return await _context.Set<AsignacionDirigente>()
                .AnyAsync(x =>
                    x.UsuarioId == usuarioId);
        }

        public async Task<List<AsignacionDirigente>>GetAllList()
        {
            return await _context.AsignacionesDirigentes
                .Include(x => x.Usuario)
                .Include(x => x.PartidoPolitico)
                .ToListAsync();
        }
    }
}
