namespace eVote360Pro.Core.Application.DTOs
{
    public class HomeAdministradorDTO
    {
        public List<int> AniosDisponibles { get; set; } = new();

        public int? Anio { get; set; }

        public List<ResumenEleccionDTO> Resumenes { get; set; } = new();
    }
}