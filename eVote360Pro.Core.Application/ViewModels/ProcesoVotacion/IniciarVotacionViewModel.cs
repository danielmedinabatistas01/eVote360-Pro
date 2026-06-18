using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.ProcesoVotacion
{
    public class IniciarVotacionViewModel
    {
        [Required(ErrorMessage = "Debe ingresar su número de documento.")]
        [Display(Name = "Número de documento de identidad")]
        public string DocumentoIdentidad { get; set; } = string.Empty;
    }
}