using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.PartidoPolitico
{
    public class SavePartidoPoliticoViewModel : BasicViewModel<int>
    {
        [Required(ErrorMessage = "Nombre obligatorio.")]
        [StringLength(150, ErrorMessage = "Máximo 150 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Siglas obligatorias.")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres.")]
        public string Siglas { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
        public string? Descripcion { get; set; }

        public string? LogoUrl { get; set; }

        public IFormFile? LogoFile { get; set; }

        public bool EsActivo { get; set; }
    }
}