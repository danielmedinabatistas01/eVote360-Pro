using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.Usuario
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe ingresar el nombre de usuario.")]
        [DataType(DataType.Text)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar la contraseña.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = string.Empty;
    }
}