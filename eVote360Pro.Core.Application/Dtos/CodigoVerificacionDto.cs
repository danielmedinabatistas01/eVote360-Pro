using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Dtos
{
    public class CodigoVerificacionDto
    {
        public int Id { get; set; }

        public int CiudadanoId { get; set; }

        public string Codigo { get; set; } = null!;

        public DateTime FechaExpiracion { get; set; }

        public bool Utilizado { get; set; }
    }
}
