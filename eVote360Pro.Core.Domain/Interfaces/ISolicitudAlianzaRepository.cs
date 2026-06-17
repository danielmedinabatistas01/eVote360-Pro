using eVote360Pro.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface ISolicitudAlianzaRepository
        : IGenericRepository<SolicitudAlianza>
    {
        Task<bool> ExisteSolicitudPendienteAsync(
            int partidoOrigenId,
            int partidoDestinoId);

        Task<List<SolicitudAlianza>>
            ObtenerPendientesAsync(
                int partidoDestinoId);
    }
}
