using eVote360Pro.Core.Domain.Entities;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface IParticipacionCiudadanoRepository : IGenericRepository<ParticipacionCiudadano>
    {
        Task<bool> CiudadanoYaVotoAsync(int ciudadanoId, int eleccionId);
        Task<bool> CiudadanoYaVotoEnCualquierEleccionAsync(int ciudadanoId);
    }
}
