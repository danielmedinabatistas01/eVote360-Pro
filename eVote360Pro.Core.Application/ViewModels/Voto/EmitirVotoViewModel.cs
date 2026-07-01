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
        public List<CandidatoVotoViewModel> CandidatosExtendidos { get; set; } = new();
    }

    public class CandidatoVotoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string FotoUrl { get; set; } = string.Empty;
        public string PartidoNombre { get; set; } = string.Empty;
        public string PartidoSiglas { get; set; } = string.Empty;
        public string PartidoLogoUrl { get; set; } = string.Empty;
    }
}