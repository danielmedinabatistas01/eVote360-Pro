namespace eVote360Pro.Core.Application.Dtos
{
    public class AsignacionCandidatoDto
    {
        public int Id { get; set; }

        public int CandidatoId { get; set; }

        public int PuestoElectivoId { get; set; }

        public int EleccionId { get; set; }

        public int PartidoPoliticoId { get; set; }
    }
}
