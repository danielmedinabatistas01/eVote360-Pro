using eVote360Pro.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Domain.Entities
{
    public class SolicitudAlianza: BaseEntity<int>
    {
        public int PartidoOrigenId { get; set; }

        public int PartidoDestinoId { get; set; }

        public string Estado { get; set; } = "Pendiente";

        public DateTime FechaSolicitud { get; set; }

        public DateTime? FechaRespuesta { get; set; }
    }
}
