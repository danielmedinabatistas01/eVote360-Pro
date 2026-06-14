using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Infrastructure.Persistence.Repositories
{
    public interface ICandidatoRepository : IGenericRepository<Candidato>
    {
        Task<List<Candidato>> GetActivosAsync();


    }
}
