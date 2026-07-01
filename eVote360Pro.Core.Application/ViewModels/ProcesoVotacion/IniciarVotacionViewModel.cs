using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.ProcesoVotacion
{
    public class IniciarVotacionViewModel
    {
        [Required(ErrorMessage = "Debe ingresar su número de documento.")]
        [Display(Name = "Número de documento de identidad")]
        [StringLength(11,MinimumLength = 11, ErrorMessage = "La cédula debe tener exactamente 11 dígitos.")]
        public string DocumentoIdentidad { get; set; } = string.Empty;
    }
}