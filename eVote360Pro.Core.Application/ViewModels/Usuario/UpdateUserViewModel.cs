
using eVote360Pro.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.Usuario
{
    public class UsuarioEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre.")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar el apellido.")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar el correo electrónico.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        public string CorreoElectronico { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar el nombre de usuario.")]
        [DataType(DataType.Text)]
        public string NombreUsuario { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", 
            ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula, un número y un carácter especial.")]
        public string? Contrasena { get; set; }

        [Compare(nameof(Contrasena), ErrorMessage = "Las contraseñas deben coincidir.")]
        [DataType(DataType.Password)]
        public string? ConfirmarContrasena { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un rol.")]
        public RolUsuario RolUsuario { get; set; }

        public int? PartidoPoliticoId { get; set; }

        public bool Estado { get; set; }
    }
}