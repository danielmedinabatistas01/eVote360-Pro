namespace eVote360Pro.Core.Application.ViewModels.Eleccion
{
    public class EleccionActivarViewModel
    {
        public int Id { get; set; }

        public required string Nombre { get; set; } = string.Empty;

        public required DateTime FechaRealizacion { get; set; }
    }
}