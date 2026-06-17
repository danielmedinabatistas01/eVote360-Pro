using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public class AsignacionCandidatoRepository: GenericRepository<AsignacionCandidato>, IAsignacionCandidatoRepository
    {
        private readonly ApplicationDbContext _context;

        public AsignacionCandidatoRepository(
            ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<List<AsignacionCandidato>>
            ObtenerPorPuestoAsync(int puestoId)
        {
            return await _context.Set<AsignacionCandidato>()
                .Where(x => x.PuestoElectivoId == puestoId)
                .ToListAsync();
        }

        public async Task<List<AsignacionCandidato>>
      ObtenerPorEleccionAsync(int eleccionId)
        {
            return await _context.Set<AsignacionCandidato>()
                .Include(x => x.Candidato)
                .Where(x => x.EleccionId == eleccionId)
                .ToListAsync();
        }

        public async Task<bool> ExisteAsignacionAsync(
            int candidatoId,
            int puestoId,
            int eleccionId)
        {
            return await _context.Set<AsignacionCandidato>()
                .AnyAsync(x =>
                    x.CandidatoId == candidatoId &&
                    x.PuestoElectivoId == puestoId &&
                    x.EleccionId == eleccionId);
        }
    }
}
