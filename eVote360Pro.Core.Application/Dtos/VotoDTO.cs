namespace eVote360Pro.Core.Application.DTOs
{
    public class VotoDTO
    {
        public int Id { get; set; }

        public int EleccionId { get; set; }

        public int CiudadanoId { get; set; }

        public DateTime FechaVoto { get; set; }

        public List<VotoDetalleDTO> VotoDetalles { get; set; } = new();

    }
}