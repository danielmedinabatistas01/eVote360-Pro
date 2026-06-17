using eVote360Pro.Core.Domain.Common;

namespace eVote360Pro.Core.Domain.Entities
{
    public class AsignacionCandidato : BaseEntity<int>
    {
        public required int CandidatoId { get; set; }
        public Candidato? Candidato { get; set; }

        public required int PuestoElectivoId { get; set; }
        public PuestoElectivo? PuestoElectivo { get; set; }

        public required int EleccionId { get; set; }
        public Eleccion? Eleccion { get; set; }

        public int PartidoPoliticoId { get; set; }

        public PartidoPolitico PartidoPolitico { get; set; }
    }
}
