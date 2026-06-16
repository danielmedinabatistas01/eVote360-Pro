using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.ViewModels.AsignacionCandidato
{
    public class SaveAsignacionCandidatoViewModel : BasicViewModel<int>
    {
        public int CandidatoId { get; set; }

        public int PuestoElectivoId { get; set; }

        public int EleccionId { get; set; }
    }
}
