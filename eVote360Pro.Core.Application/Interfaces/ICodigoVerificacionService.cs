using eVote360Pro.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface ICodigoVerificacionService
        : IGenericService<CodigoVerificacionDto>
    {
        Task<string> GenerarCodigoAsync(
            int ciudadanoId,
            int eleccionId);

        Task<bool> ValidarCodigoAsync(
            int ciudadanoId,
            int eleccionId,
            string codigo);

        Task MarcarComoUtilizadoAsync(
            int codigoId);
    }
}
