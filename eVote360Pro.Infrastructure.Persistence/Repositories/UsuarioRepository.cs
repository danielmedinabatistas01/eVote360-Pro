using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Enums;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Usuario?> LoginAsync(string nombreUsuario, string contrasena)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(x =>
                    x.NombreUsuario == nombreUsuario &&
                    x.Contrasena == contrasena);
        }

        public async Task<int> CountAdministradoresActivosAsync()
        {
            return await _context.Usuarios
                .CountAsync(x =>
                    x.RolUsuario == RolUsuario.Administrador &&
                    x.Estado);
        }
    }
}