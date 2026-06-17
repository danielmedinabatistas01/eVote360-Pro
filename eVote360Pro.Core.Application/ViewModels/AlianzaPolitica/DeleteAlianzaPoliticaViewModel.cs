namespace eVote360Pro.Core.Application.ViewModels.AlianzaPolitica
{
    public class DeleteAlianzaPoliticaViewModel
        : BasicViewModel<int>
    {
        public string PartidoOrigen { get; set; } = null!;

        public string PartidoDestino { get; set; } = null!;
    }
}