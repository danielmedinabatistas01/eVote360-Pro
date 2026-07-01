using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.ViewModels.Candidato
{
    public class DeleteCandidatoViewModel : BasicViewModel<int>
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;
    }
}
