using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.ViewModels.Voto
{
    public class SaveVotoViewModel : BasicViewModel<int>
    {
        public int EleccionId { get; set; }

        public int CiudadanoId { get; set; }

        public DateTime FechaVoto { get; set; }
    }
}
