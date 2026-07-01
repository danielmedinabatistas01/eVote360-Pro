using Microsoft.AspNetCore.Http;

namespace eVote360Pro.Core.Application.ViewModels.Candidato
{
    public class CandidatoViewModel : BasicViewModel<int>
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string FotoUrl { get; set; } = null!;

        public IFormFile? Foto { get; set; }

        public bool Estado { get; set; }

        public int PartidoPoliticoId { get; set; }
    }
}
