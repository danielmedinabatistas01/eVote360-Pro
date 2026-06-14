using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IOcrService
    {
        Task<string> ExtraerTextoAsync(
            string rutaImagen);

        Task<string?> ExtraerCedulaAsync(
            string rutaImagen);
    }
}
