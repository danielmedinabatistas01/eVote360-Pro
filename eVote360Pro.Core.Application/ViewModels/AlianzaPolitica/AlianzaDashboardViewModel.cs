using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.ViewModels.AlianzaPolitica
{
    public class AlianzaDashboardViewModel
    {
        public List<AlianzaPoliticaViewModel>
            SolicitudesPendientes
        { get; set; } = [];

        public List<AlianzaPoliticaViewModel>
            SolicitudesRealizadas
        { get; set; } = [];

        public List<AlianzaPoliticaViewModel>
            AlianzasVigentes
        { get; set; } = [];
    }
}
