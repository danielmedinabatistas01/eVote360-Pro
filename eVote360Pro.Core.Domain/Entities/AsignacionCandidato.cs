using eVote360Pro.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Domain.Entities
{
    public class AsignacionCandidato : BaseEntity
    {
        public required int CandidatoId { get; set; }
        public Candidato? Candidato { get; set; }

        public required int PuestoElectivoId { get; set; }
        public PuestoElectivo? PuestoElectivo { get; set; }

        public required int EleccionId { get; set; }
        public Eleccion? Eleccion { get; set; }
    }
}
