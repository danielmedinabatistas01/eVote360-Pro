
using eVote360Pro.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.Usuario
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Debe ingresar el nombre.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar el apellido.")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar el correo electrónico.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        public string CorreoElectronico { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar el nombre de usuario.")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar la contraseña.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe confirmar la contraseña.")]
        [Compare(nameof(Contrasena), ErrorMessage = "Las contraseñas deben coincidir.")]
        [DataType(DataType.Password)]
        public string ConfirmarContrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un rol.")]
        public RolUsuario RolUsuario { get; set; }

        public int? PartidoPoliticoId { get; set; }
    }
}