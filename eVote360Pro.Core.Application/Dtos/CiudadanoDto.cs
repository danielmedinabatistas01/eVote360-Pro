
namespace eVote360Pro.Core.Application.Dtos
{
    public class CiudadanoDto : BasicDto<int>
    {
        public required string Nombre { get; set; }

        public required string Apellido { get; set; }

        public required string NumeroIdentificacion { get; set; }

        public required string CorreoElectronico { get; set; }

        public bool EsActivo { get; set; }
    }
}
