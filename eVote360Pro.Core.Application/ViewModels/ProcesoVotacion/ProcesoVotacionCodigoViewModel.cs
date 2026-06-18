using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.ProcesoVotacion
{
    public class ProcesoVotacionCodigoViewModel
    {
        [Required(ErrorMessage = "Debe ingresar el código de verificación enviado a su correo electrónico.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "El código debe tener 6 dígitos.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "El código debe ser numérico de 6 dígitos.")]
        [Display(Name = "Código de verificación")]
        public string Codigo { get; set; } = string.Empty;

        public string? DocumentoIdentidad { get; set; }
    }
}