
using eVote360Pro.Core.Domain.Common;

namespace eVote360Pro.Core.Domain.Entities
{
    public class Voto: BaseEntity
    {
            public int EleccionId { get; set; }

            public int CiudadanoId { get; set; }

            public DateTime FechaVotacion { get; set; }

            /* Navegaciones agregar despues 
            public Eleccion Eleccion { get; set; }

            public ICollection<VotoDetalle> VotoDetalles { get; set; }
                = new List<VotoDetalle>(); */
        
    }
}
