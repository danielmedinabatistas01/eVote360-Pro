using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.ProcesoVotacion
{
    public class IniciarVotacionViewModel
    {
        [Required(ErrorMessage = "El número de documento es requerido.")]
        public string NumeroDocumento { get; set; } = string.Empty;
    }
}