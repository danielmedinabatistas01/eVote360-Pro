using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public class AlianzaPoliticaRepository: GenericRepository<AlianzaPolitica>,IAlianzaPoliticaRepository
    {
        private readonly ApplicationDbContext _context;

        public AlianzaPoliticaRepository(
            ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<AlianzaPolitica>>GetActivosAsync()
        {
            return await _context.Set<AlianzaPolitica>()
                .Where(x => x.Estado)
                .ToListAsync();
        }
    }
}
