using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;


namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public class CodigoVerificacionRepository: GenericRepository<CodigoVerificacion>,ICodigoVerificacionRepository
    {
        private readonly ApplicationDbContext _context;

        public CodigoVerificacionRepository(
            ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<CodigoVerificacion?> GetCodigoAsync(
            int ciudadanoId,
            string codigo)
        {
            return await _context.CodigosVerificacion
                .FirstOrDefaultAsync(x =>
                    x.CiudadanoId == ciudadanoId &&
                    x.Codigo == codigo &&
                    !x.Utilizado);
        }
    }
}
