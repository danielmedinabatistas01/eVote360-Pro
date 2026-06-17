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

        Task<bool> HaParticipadoEnEleccionAsync(
    int candidatoId);

        Task<bool> TieneAsignacionVigenteAsync(
            int candidatoId);

        Task<List<AsignacionCandidato>>
    ObtenerPorPartidoAsync(
        int partidoId);

        Task<bool>
            ExisteAsignacionPorPuestoAsync(
                int puestoId,
                int partidoId);

        Task<bool>
            CandidatoTieneAsignacionAsync(
                int candidatoId,
                int partidoId);

        Task<AsignacionCandidato?>
    ObtenerAsignacionOrigenAsync(
        int candidatoId);

        Task<bool>
            PerteneceAlPartidoAsync(
                int asignacionId,
                int partidoId);


    }
}
