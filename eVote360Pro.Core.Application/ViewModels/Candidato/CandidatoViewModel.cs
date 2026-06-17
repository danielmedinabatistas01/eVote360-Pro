using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.ViewModels.Candidato
{

    public class SaveCandidatoViewModel : BasicViewModel<int>
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;
        public IFormFile? Foto { get; set; }


        public string? FotoUrl { get; set; }

        public bool Estado { get; set; }

        public int PartidoPoliticoId { get; set; }
    }
}
