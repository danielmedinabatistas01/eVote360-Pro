using eVote360Pro.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote360Pro.Core.Application.DTOs;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IVotoService
    {
        Task<bool> CiudadanoYaVotoAsync(
            int ciudadanoId,
            int eleccionId);
        Task CrearVotoAsync(VotoDto dto);

        Task RegistrarVotoAsync(
            VotoDto dto);
        Task<int> CountCiudadanosVotaronAsync(int eleccionId);

        Task<bool> PuedeVotarAsync(
            int ciudadanoId,
            int eleccionId);
        Task<List<VotoDto>> GetByEleccionIdAsync(int eleccionId);
    }
}
