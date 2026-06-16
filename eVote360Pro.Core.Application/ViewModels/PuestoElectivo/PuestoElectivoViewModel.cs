namespace eVote360Pro.Core.Application.ViewModels.PuestoElectivo
{
    public class PuestoElectivoViewModel
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public required bool EsActivo { get; set; }
    }
}