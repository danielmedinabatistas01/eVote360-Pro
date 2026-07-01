using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.Candidato
{
    public class CandidatoViewModel : BasicViewModel<int>
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50)]
        public string Apellido { get; set; } = null!;

        public string FotoUrl { get; set; } = null!;
        [Required(ErrorMessage = "Debe seleccionar una fotografía.")]
        public IFormFile? Foto { get; set; }

        public bool Estado { get; set; }

        public int PartidoPoliticoId { get; set; }
    }
}
