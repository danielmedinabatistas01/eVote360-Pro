using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.ViewModels.CodigoVerificacion
{
    public class DeleteCodigoVerificacionViewModel : BasicViewModel<int>
    {
        public string Codigo { get; set; } = null!;
    }
}
