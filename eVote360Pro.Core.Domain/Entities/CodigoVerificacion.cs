using eVote360Pro.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Domain.Entities
{
    public class CodigoVerificacion : BaseEntity<int>
    {

        public int CiudadanoId { get; set; }
        public Ciudadano? Ciudadano { get; set; }

        public int EleccionId { get; set; }

        public string Codigo { get; set; } = null!;

        public DateTime FechaGeneracion { get; set; }

        public DateTime FechaExpiracion { get; set; }

        public bool Utilizado { get; set; }

        public Eleccion? Eleccion { get; set; }
    }
}
