using eVote360Pro.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IVotoService
    {
        Task<bool> CiudadanoYaVotoAsync(
            int ciudadanoId,
            int eleccionId);

        Task RegistrarVotoAsync(
            VotoDto dto);

        Task<bool> PuedeVotarAsync(
            int ciudadanoId,
            int eleccionId);
    }
}
