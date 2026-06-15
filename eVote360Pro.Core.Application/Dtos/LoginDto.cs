namespace eVote360Pro.Core.Application.Dtos.User
{
    public class LoginDto
    {
        public required string NombreUsuario { get; set; }
        public required string Contrasena { get; set; }
    }
}