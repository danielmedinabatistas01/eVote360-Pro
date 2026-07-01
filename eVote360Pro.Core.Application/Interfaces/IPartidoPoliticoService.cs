using eVote360Pro.Core.Application.Dtos;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IPartidoPoliticoService
        : IGenericService<PartidoPoliticoDto>
    {
        Task ActivarAsync(int id);
    }
}