using eVote360Pro.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface IVotoRepository : IGenericRepository<Voto>
    {
        Task<bool> CiudadanoYaVotoAsync(int ciudadanoId, int eleccionId);

        Task<int> CountCiudadanosVotaronAsync(int eleccionId);

        Task<List<Voto>> GetByEleccionIdAsync(int eleccionId);
    }
}
