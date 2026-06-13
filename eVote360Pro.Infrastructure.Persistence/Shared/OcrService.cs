using eVote360Pro.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Infrastructure.Persistence.Shared
{
    public class OcrService : IOcrService
    {
        public Task<string> ExtraerTextoAsync(
            string rutaImagen)
        {
            throw new NotImplementedException();
        }

        public Task<string?> ExtraerCedulaAsync(
            string rutaImagen)
        {
            throw new NotImplementedException();
        }
    }
}
