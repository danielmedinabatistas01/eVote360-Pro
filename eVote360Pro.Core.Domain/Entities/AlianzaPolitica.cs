using eVote360Pro.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Domain.Entities
{
    public class AlianzaPolitica : BaseEntity
    {
        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public bool Estado { get; set; }

        //public ICollection<PartidoPolitico>? Partidos { get; set; }
    }
}
