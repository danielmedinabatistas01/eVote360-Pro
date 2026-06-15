namespace eVote360Pro.Core.Application.DTOs
{
    public class VotoDetalleDTO
    {
        public int Id { get; set; }

        public required int VotoId { get; set; }

        public required int PuestoElectivoId { get; set; }

        public  int? CandidatoId { get; set; }
    }
}