

namespace eVote360Pro.Core.Application.ViewModels.AlianzaPolitica
{
    public class AlianzaPoliticaViewModel: BasicViewModel<int>
    {
        public int PartidoOrigenId { get; set; }

        public string PartidoOrigen { get; set; } = null!;

        public int PartidoDestinoId { get; set; }

        public string PartidoDestino { get; set; } = null!;

        public string Estado { get; set; } = "Pendiente";

        public DateTime FechaSolicitud { get; set; }

        public DateTime? FechaRespuesta { get; set; }

        public bool Vigente { get; set; }
    }
}