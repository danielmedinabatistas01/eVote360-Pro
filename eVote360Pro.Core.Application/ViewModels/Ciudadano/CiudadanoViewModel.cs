namespace eVote360Pro.Core.Application.ViewModels.Ciudadano
{
    public class CiudadanoViewModel
    {
        public int Id { get; set; }

        public required string NombreCompleto { get; set; }

        public required string NumeroIdentidad { get; set; }

        public required string CorreoElectronico { get; set; }

        public required bool EsActivo { get; set; }
    }
}