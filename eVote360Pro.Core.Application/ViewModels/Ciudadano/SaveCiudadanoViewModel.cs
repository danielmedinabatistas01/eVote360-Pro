using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.Ciudadano
{
    public class SaveCiudadanoViewModel : BasicViewModel<int>
    {
        [Required(ErrorMessage = "Nombres obligatorios.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "Apellidos obligatorios.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Apellido { get; set; } = null!;
        [Required(ErrorMessage = "Cédula obligatoria.")]
        [RegularExpression(@"^\d{3}-?\d{7}-?\d{1}$", ErrorMessage = "Formato inválido.")]
        public string NumeroIdentificacion { get; set; } = null!;
        [Required(ErrorMessage = "Correo obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo inválido.")]
        public string CorreoElectronico { get; set; } = null!;
        public bool EsActivo { get; set; }
    }
}