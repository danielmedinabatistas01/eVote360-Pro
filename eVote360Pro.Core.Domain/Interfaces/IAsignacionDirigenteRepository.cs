using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface IAsignacionDirigenteRepository: IGenericRepository<AsignacionDirigente>
    {
        Task<bool> ExisteAsignacionAsync(
            int usuarioId,
            int partidoId);

        Task<bool> PartidoTieneDirigenteAsync(
            int partidoId);

        Task<bool> UsuarioTienePartidoAsync(
            int usuarioId);

    }
}
