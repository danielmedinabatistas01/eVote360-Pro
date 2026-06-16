
using eVote360Pro.Core.Domain.Common;

namespace eVote360Pro.Core.Domain.Entities
{
    public class Voto: BaseEntity<int>
    {
            public required int EleccionId { get; set; }
            public required int CiudadanoId { get; set; }
            public DateTime FechaVotacion { get; set; }
            public Eleccion? Eleccion { get; set; }
            public Ciudadano? Ciudadano { get; set; }

        public ICollection<VotoDetalle>? VotoDetalles { get; set; }
                = new List<VotoDetalle>();
        
    }
}
