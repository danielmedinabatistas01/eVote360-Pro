

namespace eVote360Pro.Core.Application.Dtos
{
    public class VotoDto
    {
        public int Id { get; set; }

        public required int EleccionId { get; set; }

        public required int CiudadanoId { get; set; }    

        public DateTime FechaVotacion { get; set; }
    }
}
