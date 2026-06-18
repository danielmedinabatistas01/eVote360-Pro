using Microsoft.AspNetCore.Mvc.Rendering;

namespace eVote360Pro.Core.Application.ViewModels.Voto
{
    public class EmitirVotoViewModel
    {
        public int CiudadanoId { get; set; }

        public int EleccionId { get; set; }

        public List<SeleccionVotoViewModel>Selecciones{ get; set; }= new();
    }

    public class SeleccionVotoViewModel
    {
        public int PuestoElectivoId { get; set; }

        public string NombrePuesto { get; set; }
            = string.Empty;

        public int? CandidatoId { get; set; }

        public List<SelectListItem>Candidatos{ get; set; }= new();
    }
}