using eVote360Pro.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IAsignacionCandidatoService : IGenericService<AsignacionCandidatoDto>
    {
        Task AsignarCandidatoAsync(AsignacionCandidatoDto dto,int partidoDirigenteId);

        Task EliminarAsignacionAsync(int asignacionId,int partidoDirigenteId);

        Task<List<AsignacionCandidatoDto>>ObtenerPorEleccionAsync(int eleccionId);

        Task<List<AsignacionCandidatoDto>>ObtenerPorPuestoAsync(int puestoId);

        Task<bool> ExisteAsignacionAsync(
            int candidatoId,
            int puestoId,
            int eleccionId);
    }
}
