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
        public required string Nombre { get; set; } = null!;

        public required string Apellido { get; set; } = null!;

        public required string FotoUrl { get; set; } = null!;

        public required bool Estado { get; set; }

        public required int PartidoPoliticoId { get; set; }


        public PartidoPolitico? PartidoPolitico { get; set; }
        public ICollection<AsignacionCandidato>? Asignaciones { get; set; }
    }
}
