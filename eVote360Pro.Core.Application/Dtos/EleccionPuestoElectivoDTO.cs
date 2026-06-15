namespace eVote360Pro.Core.Application.DTOs
{
    public class EleccionPuestoElectivoDTO
    {
        public int Id { get; set; }

        public required int EleccionId { get; set; }

        public required int PuestoElectivoId { get; set; }
    }
}