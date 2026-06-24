
namespace eVote360Pro.Core.Application.Dtos
{
    public class PuestoElectivoDto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public required bool EsActivo { get; set; }
    }
}
