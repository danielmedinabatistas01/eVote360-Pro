using eVote360Pro.Core.Domain.Common;

namespace eVote360Pro.Core.Domain.Entities
{
    public class Ciudadano: BaseEntity
    {
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string CorreoElectronico { get; set; }
        public required string NumeroIdentificacion { get; set; }
        public required bool EsActivo { get; set; }
        //public ICollection<Voto> Votos { get; set; }
        //public ICollection<CodigoVerificacion> CodigosVerificacion { get; set; }
    }
}
