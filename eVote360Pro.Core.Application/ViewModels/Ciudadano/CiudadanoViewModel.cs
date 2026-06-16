namespace eVote360Pro.Core.Application.ViewModels.Ciudadano
{
    public class CiudadanoViewModel: BasicViewModel<int>
    {

        public required string NombreCompleto { get; set; }

        public required string NumeroIdentidad { get; set; }

        public required string CorreoElectronico { get; set; }

        public required bool EsActivo { get; set; }
    }
}