using eVote360Pro.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.Usuario
{
    public class UsuarioCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del usuario.")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar el apellido del usuario.")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar el correo electrónico del usuario.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        public string CorreoElectronico { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar el nombre de usuario.")]
        [DataType(DataType.Text)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe ingresar la contraseña.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 100 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).+$",
     ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula, un número y un carácter especial.")]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe confirmar la contraseña.")]
        [Compare(nameof(Contrasena), ErrorMessage = "Las contraseñas deben coincidir.")]
        [DataType(DataType.Password)]
        public string ConfirmarContrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un rol.")]
        public RolUsuario RolUsuario { get; set; }

        public int? PartidoPoliticoId { get; set; }

        public bool Estado { get; set; } = true;
    }
}