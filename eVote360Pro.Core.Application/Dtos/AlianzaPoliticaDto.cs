using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Dtos
{
    public class AlianzaPoliticaDto
    {
        public int Id { get; set; }

        public int PartidoOrigenId { get; set; }

        public int PartidoDestinoId { get; set; }

        public string Estado { get; set; } = "Pendiente";

        public DateTime FechaSolicitud { get; set; }

        public DateTime? FechaRespuesta { get; set; }

        public bool Vigente { get; set; }
    }
}