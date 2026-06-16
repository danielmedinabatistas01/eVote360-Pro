using eVote360Pro.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface ICodigoVerificacionRepository: IGenericRepository<CodigoVerificacion>
    {
        Task<CodigoVerificacion?> GetCodigoAsync(
            int ciudadanoId,
            int eleccionId,
            string codigo);
    }
}
