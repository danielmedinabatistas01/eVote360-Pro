using eVote360Pro.Core.Domain.Enums;

namespace eVote360Pro.Core.Application.ViewModels.Eleccion
{
    public class EleccionIndexViewModel
    {
        public int Id { get; set; }

        public required string Nombre { get; set; } = string.Empty;

        public required DateTime FechaRealizacion { get; set; }

        public EstadoEleccion EstadoEleccion { get; set; }
    }
}