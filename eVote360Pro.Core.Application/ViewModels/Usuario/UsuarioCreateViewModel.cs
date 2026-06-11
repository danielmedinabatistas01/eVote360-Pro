
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
        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = string.Empty;

        [Compare(nameof(Contrasena), ErrorMessage = "Las contraseñas deben coincidir.")]
        [Required(ErrorMessage = "Debe confirmar la contraseña.")]
        [DataType(DataType.Password)]
        public string ConfirmarContrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un rol.")]
        public RolUsuario RolUsuario { get; set; }

        public int? PartidoPoliticoId { get; set; }

        public bool Estado { get; set; } = true;
    }
}