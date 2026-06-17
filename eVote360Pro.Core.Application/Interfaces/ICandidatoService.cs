using eVote360Pro.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface ICandidatoService : IGenericService<CandidatoDto>
    {

        Task ActivarCandidatoAsync(int id);

        Task DesactivarCandidatoAsync(int id);

        Task<List<CandidatoDto>> GetActivosAsync();

        Task<List<CandidatoDto>> GetByPartidoPoliticoAsync(int partidoPoliticoId);
    }
}