using eVote360Pro.Core.Domain.Common;

namespace eVote360Pro.Core.Domain.Entities
{
    public class AsignacionDirigente: BaseEntity<int>
    {
        public required int UsuarioId { get; set; }
        public required int PartidoPoliticoId { get; set; }
        public  Usuario? Usuario { get; set; } 
        public  PartidoPolitico? PartidoPolitico { get; set; }
    }
}
