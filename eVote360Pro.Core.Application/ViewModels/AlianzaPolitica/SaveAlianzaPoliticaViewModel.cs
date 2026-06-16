using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.ViewModels.AlianzaPolitica
{
    public class SaveAlianzaPoliticaViewModel : BasicViewModel<int>
    {
        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public bool Estado { get; set; }
    }
}
