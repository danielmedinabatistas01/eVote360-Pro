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
    public class AsignacionCandidatoRepository : GenericRepository<AsignacionCandidato>, IAsignacionCandidatoRepository
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
            return await _context.AsignacionesCandidatos
                .Include(x => x.Candidato)
                .Include(x => x.PuestoElectivo)
                .Include(x => x.PartidoPolitico)
                .Include(x => x.Eleccion)
                .Where(x => x.EleccionId == eleccionId)
                .ToListAsync();
        }

        public async Task<List<AsignacionCandidato>>
GetAllByEleccionAsync(int eleccionId)
        {
            return await _context.AsignacionesCandidatos
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

        public async Task<AsignacionCandidato?>
    ObtenerAsignacionOrigenAsync(
    int candidatoId)
        {
            return await _context
                .AsignacionesCandidatos
                .FirstOrDefaultAsync(x =>
                    x.CandidatoId ==
                    candidatoId);
        }

        public async Task<bool>
    PerteneceAlPartidoAsync(
    int asignacionId,
    int partidoId)
        {
            return await _context
                .AsignacionesCandidatos
                .AnyAsync(x =>
                    x.Id == asignacionId
                    &&
                    x.PartidoPoliticoId ==
                    partidoId);
        }


        public async Task<bool>
        TieneAsignacionVigenteAsync(
        int candidatoId)
        {
            return await _context
                .AsignacionesCandidatos
                .AnyAsync(x =>
                    x.CandidatoId ==
                    candidatoId);
        }


        public async Task<bool>
        CandidatoTieneAsignacionAsync(
        int candidatoId,
        int partidoId)
        {
            return await _context
                .AsignacionesCandidatos
                .AnyAsync(x =>
                    x.CandidatoId ==
                    candidatoId
                    &&
                    x.PartidoPoliticoId ==
                    partidoId);
        }



        public async Task<bool>
        ExisteAsignacionPorPuestoAsync(
        int puestoId,
        int partidoId)
        {
            return await _context
                .AsignacionesCandidatos
                .AnyAsync(x =>
                    x.PuestoElectivoId ==
                    puestoId
                    &&
                    x.PartidoPoliticoId ==
                    partidoId);
        }

        public async Task<List<AsignacionCandidato>>
            ObtenerPorPartidoAsync(
            int partidoId)
        {
            return await _context
                .AsignacionesCandidatos
                .Where(x =>
                    x.PartidoPoliticoId ==
                    partidoId)
                .ToListAsync();
        }

        public async Task<bool>
    HaParticipadoEnEleccionAsync(
    int candidatoId)
        {
            return await _context
                .AsignacionesCandidatos
                .AnyAsync(x =>
                    x.CandidatoId ==
                    candidatoId);
        }

    
    public async Task<List<AsignacionCandidato>> GetAllList()
        {
            return await _context.AsignacionesCandidatos
                .Include(x => x.Candidato)
                .Include(x => x.PuestoElectivo)
                .Include(x => x.Eleccion)
                .ToListAsync();
        }

        public async Task<bool> ExisteAsignacionAliadaAsync(int partidoOrigenId, int partidoDestinoId)
        {
            return await _context.AsignacionesCandidatos
                .Include(x => x.Candidato)
                .AnyAsync(x => 
                    x.Candidato != null && 
                    ((x.Candidato.PartidoPoliticoId == partidoOrigenId && x.PartidoPoliticoId == partidoDestinoId) ||
                     (x.Candidato.PartidoPoliticoId == partidoDestinoId && x.PartidoPoliticoId == partidoOrigenId))
                );
        }
    }
}
