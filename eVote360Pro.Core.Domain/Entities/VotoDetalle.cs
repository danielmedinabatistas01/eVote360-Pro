
using eVote360Pro.Core.Domain.Common;

namespace eVote360Pro.Core.Domain.Entities
{
    public class VotoDetalle: BaseEntity
    {
            public int VotoId { get; set; }

            public int PuestoElectivoId { get; set; }

            public int? CandidatoId { get; set; }

            public Voto Voto { get; set; }
        
    }
}
