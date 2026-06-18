using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.ProcesoVotacion
{
    public class SeleccionPuestoViewModel
    {
        public int PuestoElectivoId { get; set; }

        public string NombrePuesto { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un candidato antes de votar.")]
        [Display(Name = "Candidato seleccionado")]
        public int? CandidatoSeleccionadoId { get; set; }

        public List<CandidatoSeleccionViewModel> Candidatos { get; set; } = new();

        public string? DocumentoIdentidad { get; set; }
    }
}