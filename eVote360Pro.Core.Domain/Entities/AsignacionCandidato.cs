using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Domain.Entities
{
    public class AsignacionCandidato
    {
            public int Id { get; set; }

            public int CandidatoId { get; set; }
            public Candidato? Candidato { get; set; }

            public int PuestoElectivoId { get; set; }
            //public PuestoElectivo? PuestoElectivo { get; set; }

            public int EleccionId { get; set; }
            //public Eleccion? Eleccion { get; set; }
    }
}
