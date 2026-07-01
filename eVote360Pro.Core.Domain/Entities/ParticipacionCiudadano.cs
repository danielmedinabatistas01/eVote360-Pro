using eVote360Pro.Core.Domain.Common;
using System;

namespace eVote360Pro.Core.Domain.Entities
{
    public class ParticipacionCiudadano : BaseEntity<int>
    {
        public required int CiudadanoId { get; set; }
        public required int EleccionId { get; set; }
        public DateTime FechaVotacion { get; set; }

        public Ciudadano? Ciudadano { get; set; }
        public Eleccion? Eleccion { get; set; }
    }
}
