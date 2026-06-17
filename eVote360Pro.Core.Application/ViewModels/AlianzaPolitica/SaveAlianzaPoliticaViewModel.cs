namespace eVote360Pro.Core.Application.ViewModels.AlianzaPolitica
{
    public class SolicitudAlianzaViewModel
    {
        public int Id { get; set; }

        public string PartidoOrigen { get; set; } = null!;

        public string PartidoDestino { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public DateTime FechaSolicitud { get; set; }
    }
}