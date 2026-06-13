using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Domain
{
    public interface IVotoRepository: IGenericRepository<Voto>
    {
        Task<bool> CiudadanoYaVotoAsync(
            int ciudadanoId,
            int eleccionId);
    }
}
