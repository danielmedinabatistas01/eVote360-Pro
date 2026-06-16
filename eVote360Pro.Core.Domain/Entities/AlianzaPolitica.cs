using eVote360Pro.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Domain.Entities
{
    public class AlianzaPolitica : BaseEntity<int>
    {
        public required string Nombre { get; set; } = null!;

        public required string Descripcion { get; set; } = null!;

        public required bool Estado { get; set; }

        public ICollection<PartidoPolitico>? Partidos { get; set; }
    }
}
