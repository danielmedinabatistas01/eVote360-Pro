using eVote360Pro.Core.Domain.Common;

namespace eVote360Pro.Core.Domain.Entities
{
    public class Voto : BaseEntity<int>
    {
        public int CiudadanoId { get; set; }

        public Ciudadano? Ciudadano { get; set; }

        public int EleccionId { get; set; }

        public Eleccion? Eleccion { get; set; }

        public DateTime FechaVotacion { get; set; }

        public ICollection<VotoDetalle> VotoDetalles { get; set; }
            = new List<VotoDetalle>();
    }
}