using System.ComponentModel.DataAnnotations;

namespace eVote360Pro.Core.Application.ViewModels.AlianzaPolitica
{
    public class SaveAlianzaPoliticaViewModel: BasicViewModel<int>
    {
        [Required]
        public int PartidoOrigenId { get; set; }

        [Required]
        public int PartidoDestinoId { get; set; }
    }
}