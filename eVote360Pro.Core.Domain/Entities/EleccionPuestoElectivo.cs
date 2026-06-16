using eVote360Pro.Core.Domain.Common;

namespace eVote360Pro.Core.Domain.Entities
{
    public class EleccionPuestoElectivo : BaseEntity<int>
    {
        public int EleccionId { get; set; }

        public int PuestoElectivoId { get; set; }

        public Eleccion Eleccion { get; set; }


        public PuestoElectivo PuestoElectivo { get; set; }
    }
}
