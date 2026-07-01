using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public class ParticipacionCiudadanoRepository 
        : GenericRepository<ParticipacionCiudadano>, 
          IParticipacionCiudadanoRepository
    {
        private readonly ApplicationDbContext _context;

        public ParticipacionCiudadanoRepository(ApplicationDbContext context) 
            : base(context)
        {
            _context = context;
        }

        public async Task<bool> CiudadanoYaVotoAsync(int ciudadanoId, int eleccionId)
        {
            return await _context.ParticipacionCiudadanos
                .AnyAsync(x => x.CiudadanoId == ciudadanoId && x.EleccionId == eleccionId);
        }

        public async Task<bool> CiudadanoYaVotoEnCualquierEleccionAsync(int ciudadanoId)
        {
            return await _context.ParticipacionCiudadanos
                .AnyAsync(x => x.CiudadanoId == ciudadanoId);
        }
    }
}
