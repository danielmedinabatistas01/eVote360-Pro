using eVote360Pro.Core.Application.Dtos;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface ICiudadanoService
        : IGenericService<CiudadanoDto>
    {
        Task ActivarAsync(int id);
    }
}