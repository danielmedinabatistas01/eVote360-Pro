using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.Usuario
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; } = string.Empty;
    }
}