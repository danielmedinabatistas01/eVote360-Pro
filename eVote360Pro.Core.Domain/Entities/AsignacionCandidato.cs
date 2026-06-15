using eVote360Pro.Core.Domain.Common;

namespace eVote360Pro.Core.Domain.Entities
{
    public class AsignacionCandidato : BaseEntity
    {
        public int CandidatoId { get; set; }
        public Candidato? Candidato { get; set; }

        public int PuestoElectivoId { get; set; }
        public PuestoElectivo? PuestoElectivo { get; set; }

        public int EleccionId { get; set; }
        public Eleccion? Eleccion { get; set; }
    }
}
