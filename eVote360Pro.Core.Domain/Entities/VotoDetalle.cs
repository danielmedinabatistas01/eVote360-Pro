
using eVote360Pro.Core.Domain.Common;

namespace eVote360Pro.Core.Domain.Entities
{
    public class VotoDetalle : BaseEntity<int>
    {
        public int VotoId { get; set; }
        public Voto? Voto { get; set; }

        public int PuestoElectivoId { get; set; }
        public PuestoElectivo? PuestoElectivo { get; set; }

        public int? CandidatoId { get; set; }
        public Candidato? Candidato { get; set; }
    }
}
