using eVote360Pro.Core.Domain.Common;

namespace eVote360Pro.Core.Domain.Entities
{
    public class PuestoElectivo: BaseEntity<int>
    {
        public required string  Nombre { get; set; }
        public required string Descripcion { get; set; }
        public required bool EsActivo { get; set; }
        public ICollection<AsignacionCandidato> AsignacionesCandidatos { get; set; }
            = new List<AsignacionCandidato>();

        public ICollection<EleccionPuestoElectivo> Elecciones { get; set; }
            = new List<EleccionPuestoElectivo>();
    }
}