namespace eVote360Pro.Core.Application.ViewModels.Ciudadano
{
    public class SaveCiudadanoViewModel: BasicViewModel<int>
    {

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string NumeroIdentidad { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;

        public bool EsActivo { get; set; } 
    }
}