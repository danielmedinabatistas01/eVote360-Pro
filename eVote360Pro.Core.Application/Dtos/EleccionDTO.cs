using eVote360Pro.Core.Domain.Enums;

namespace eVote360Pro.Core.Application.DTOs
{
    public class EleccionDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public DateTime FechaRealizacion { get; set; }

        public EstadoEleccion EstadoEleccion { get; set; }
    }
}