using eVote360Pro.Core.Application.Dtos;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IPuestoElectivoService
        : IGenericService<PuestoElectivoDto>
    {
        Task ActivarAsync(int id);
    }
}