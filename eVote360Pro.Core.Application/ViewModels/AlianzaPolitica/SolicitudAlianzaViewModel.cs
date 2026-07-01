using Microsoft.AspNetCore.Mvc.Rendering;


namespace eVote360Pro.Core.Application.ViewModels.AlianzaPolitica
{
    public class SaveAlianzaPoliticaViewModel : BasicViewModel<int>
    {
        public int PartidoOrigenId { get; set; }

        public int PartidoDestinoId { get; set; }

        public List<SelectListItem> PartidosDisponibles { get; set; }
            = new();
    }
}