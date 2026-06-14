using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface ICodigoVerificacionService
    {
        Task<string> GenerarCodigoAsync(
            int ciudadanoId);

        Task<bool> ValidarCodigoAsync(
            int ciudadanoId,
            string codigo);

        Task MarcarComoUtilizadoAsync(
            int codigoId);
    }
}
