using eVote360Pro.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IAlianzaPoliticaService : IGenericService<AlianzaPoliticaDto>
    {

        Task ActivarAsync(int id);

        Task DesactivarAsync(int id);

        Task<List<AlianzaPoliticaDto>> GetActivosAsync();
    }
}
