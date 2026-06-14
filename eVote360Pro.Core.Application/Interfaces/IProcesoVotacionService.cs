using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IProcesoVotacionService
    {
        Task<bool> ValidarCedulaAsync(
    string numeroDocumento);

        Task<bool> ValidarIdentidadOcrAsync(
            string numeroDocumento,
            string rutaImagen);

        Task<string> GenerarCodigoAsync(
            int ciudadanoId);

        Task<bool> ValidarCodigoAsync(
            int ciudadanoId,
            string codigo);
    }
}
