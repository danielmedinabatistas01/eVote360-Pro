using eVote360Pro.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface IAsignacionCandidatoRepository: IGenericRepository<AsignacionCandidato>
    {
        Task<List<AsignacionCandidato>>
            ObtenerPorEleccionAsync(int eleccionId);

        Task<List<AsignacionCandidato>>
            ObtenerPorPuestoAsync(int puestoId);

        Task<bool> ExisteAsignacionAsync(
            int candidatoId,
            int puestoId,
            int eleccionId);
    }
}
