using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Dtos
{
    public class VotoDto
    {
        public int Id { get; set; }

        public int EleccionId { get; set; }

        public int CiudadanoId { get; set; }    

        public DateTime FechaVotacion { get; set; }
    }
}
