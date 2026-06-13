using eVote360Pro.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IAlianzaPoliticaService
    {
        Task<List<AlianzaPoliticaDto>> GetAllAsync();

        Task<AlianzaPoliticaDto?> GetByIdAsync(int id);

        Task AddAsync(AlianzaPoliticaDto dto);

        Task UpdateAsync(int id, AlianzaPoliticaDto dto);

        Task DeleteAsync(int id);

        Task ActivarAsync(int id);

        Task DesactivarAsync(int id);

        Task<List<AlianzaPoliticaDto>> GetActivosAsync();
    }
}
