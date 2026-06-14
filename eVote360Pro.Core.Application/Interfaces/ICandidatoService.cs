using eVote360Pro.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface ICandidatoService
    {
        Task<List<CandidatoDto>> GetAllAsync();

        Task<CandidatoDto?> GetByIdAsync(int id);

        Task AddAsync(CandidatoDto dto);

        Task UpdateAsync(int id, CandidatoDto dto);

        Task DeleteAsync(int id);

        Task ActivarCandidatoAsync(int id);

        Task DesactivarCandidatoAsync(int id);

        Task<List<CandidatoDto>> GetActivosAsync();

    }
}
