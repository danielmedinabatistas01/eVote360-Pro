
using eVote360Pro.Core.Domain.Enums;

namespace eVote360Pro.Core.Application.DTOs
{
    public class EleccionDTO
    {
        public int Id { get; set; }

        public required string Nombre { get; set; } = string.Empty;

        public required DateTime FechaRealizacion { get; set; }

        public required EstadoEleccion EstadoEleccion { get; set; }
    }
}