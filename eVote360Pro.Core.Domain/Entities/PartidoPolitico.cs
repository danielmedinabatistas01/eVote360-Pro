using eVote360Pro.Core.Domain.Common;

namespace eVote360Pro.Core.Domain.Entities
{
    public class PartidoPolitico : BaseEntity
    {
        public required string Nombre { get; set; }
        public required string Siglas { get; set; }
        public string? Descripcion { get; set; }
        public required string LogoUrl { get; set; }
        public required bool EsActivo { get; set; }
        public required AsignacionDirigente AsignacionDirigente { get; set; }
        public ICollection<Candidato> Candidatos { get; set; }
        public ICollection<AlianzaPolitica> Alianzas { get; set; }
    }
}
