using eVote360Pro.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Domain.Entities
{
    public class Candidato : BaseEntity
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string FotoUrl { get; set; } = null!;

        public bool Estado { get; set; }

        public int PartidoPoliticoId { get; set; }


        public PartidoPolitico? PartidoPolitico { get; set; }
        public ICollection<AsignacionCandidato>? Asignaciones { get; set; }
        public ICollection<Voto>? Votos { get; set; }
    }
}
