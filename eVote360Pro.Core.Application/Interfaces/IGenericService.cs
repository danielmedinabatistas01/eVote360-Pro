using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IGenericService<TDto>
    {
        Task<List<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(int id);
        Task AddAsync(TDto dto);
        Task UpdateAsync(int id, TDto dto);
        Task DeleteAsync(int id);
    }
}
