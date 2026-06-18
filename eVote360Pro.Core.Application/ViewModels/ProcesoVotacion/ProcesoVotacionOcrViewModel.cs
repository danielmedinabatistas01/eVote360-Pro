using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.ProcesoVotacion
{
    public class ProcesoVotacionOcrViewModel
    {
        [Required(ErrorMessage = "Debe subir una imagen de su cédula para validar su identidad.")]
        [Display(Name = "Imagen de la cédula")]
        public IFormFile? ImagenCedula { get; set; }

        public string? DocumentoIdentidad { get; set; }
    }
}