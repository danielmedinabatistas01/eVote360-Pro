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
    public class AlianzaPoliticaRepository : GenericRepository<AlianzaPolitica>, IAlianzaPoliticaRepository
    {
        private readonly ApplicationDbContext _context;

        public AlianzaPoliticaRepository(
            ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<bool>
            ExisteAlianzaAsync(
            int partidoOrigenId,
            int partidoDestinoId)
        {
            return await _context
                .AlianzasPoliticas
                .AnyAsync(x =>
                    x.Vigente &&
                    (
                        (x.PartidoOrigenId ==
                         partidoOrigenId &&
                         x.PartidoDestinoId ==
                         partidoDestinoId)

                        ||

                        (x.PartidoOrigenId ==
                         partidoDestinoId &&
                         x.PartidoDestinoId ==
                         partidoOrigenId)
                    ));
        }

        public async Task<bool>
    ExisteSolicitudPendienteAsync(
    int partidoOrigenId,
    int partidoDestinoId)
        {
            return await _context
                .AlianzasPoliticas
                .AnyAsync(x =>
                    x.PartidoOrigenId ==
                    partidoOrigenId

                    &&

                    x.PartidoDestinoId ==
                    partidoDestinoId

                    &&

                    x.Estado ==
                    "Pendiente");
        }

        public async Task<List<AlianzaPolitica>>
    ObtenerPendientesAsync(
    int partidoDestinoId)
        {
            return await _context
                .AlianzasPoliticas
                .Where(x =>
                    x.PartidoDestinoId ==
                    partidoDestinoId

                    &&

                    x.Estado ==
                    "Pendiente")
                .ToListAsync();
        }

        public async Task<List<AlianzaPolitica>>
    GetActivosAsync()
        {
            return await _context
                .AlianzasPoliticas
                .Where(x => x.Vigente)
                .ToListAsync();
        }

        public async Task<List<AlianzaPolitica>>
        ObtenerSolicitudesPendientesAsync(int partidoId)
        {
            return await _context.AlianzasPoliticas
                .Include(x => x.PartidoOrigen)
                .Include(x => x.PartidoDestino)
                .Where(x =>
                    x.PartidoDestinoId == partidoId &&
                    x.Estado == "Pendiente")
                .ToListAsync();
        }


        public async Task<List<AlianzaPolitica>>
        ObtenerSolicitudesRealizadasAsync(int partidoId)
        {
            return await _context.AlianzasPoliticas
                .Include(x => x.PartidoOrigen)
                .Include(x => x.PartidoDestino)
                .Where(x =>
                    x.PartidoOrigenId == partidoId &&
                    x.Estado == "Pendiente")
                .ToListAsync();
        }


        public async Task<List<AlianzaPolitica>>
         ObtenerAlianzasVigentesAsync(int partidoId)
        {
            return await _context.AlianzasPoliticas
                .Include(x => x.PartidoOrigen)
                .Include(x => x.PartidoDestino)
                .Where(x =>
                    x.Vigente &&
                    (
                        x.PartidoOrigenId == partidoId ||
                        x.PartidoDestinoId == partidoId
                    ))
                .ToListAsync();
        }
    }
}
