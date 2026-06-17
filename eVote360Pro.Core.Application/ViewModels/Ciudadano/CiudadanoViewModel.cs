namespace eVote360Pro.Core.Application.ViewModels.Ciudadano
{
    public class CiudadanoViewModel: BasicViewModel<int>
    {

        public required string Nombre { get; set; }

        public required string Apellido { get; set; }
        public required string NumeroIdentificacion { get; set; }

        public required string CorreoElectronico { get; set; }

        public required bool EsActivo { get; set; }
    }
}