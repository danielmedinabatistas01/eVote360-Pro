
using eVote360Pro.Core.Domain.Common;
using eVote360Pro.Core.Domain.Enums;

namespace eVote360Pro.Core.Domain.Entities
{
    public class Eleccion: BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;

        public DateTime FechaRealizacion { get; set; }

        public EstadoEleccion EstadoEleccion { get; set; }


        // Relaciones

        public ICollection<EleccionPuestoElectivo> PuestosElectivos { get; set; }
            = new List<EleccionPuestoElectivo>();
      

        public ICollection<Voto> Votos { get; set; }
            = new List<Voto>();


    }
}
