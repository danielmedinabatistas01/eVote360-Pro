
using eVote360Pro.Core.Domain.Common;
using eVote360Pro.Core.Domain.Enums;

namespace eVote360Pro.Core.Domain.Entities
{
    public class Eleccion: BaseEntity<int>
    {
        public required string Nombre { get; set; } = string.Empty;

        public required DateTime FechaRealizacion { get; set; }

        public required EstadoEleccion EstadoEleccion { get; set; } = EstadoEleccion.Pendiente;


        public ICollection<EleccionPuestoElectivo> PuestosElectivos { get; set; }
            = new List<EleccionPuestoElectivo>();
      

        public ICollection<Voto> Votos { get; set; }
            = new List<Voto>();

        public ICollection<ParticipacionCiudadano> Participaciones { get; set; }
            = new List<ParticipacionCiudadano>();



    }
}
