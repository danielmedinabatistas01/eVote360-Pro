namespace eVote360Pro.Core.Application.ViewModels.Ciudadano
{
    public class SaveCiudadanoViewModel
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public required string Apellido { get; set; }

        public required string NumeroIdentidad { get; set; }

        public required string CorreoElectronico { get; set; }

        public required bool EsActivo { get; set; }
    }
}