using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario?> LoginAsync(string nombreUsuario, string contrasena);

        Task<int> CountAdministradoresActivosAsync();

        Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario, int? idExcluir = null);

        Task<bool> ExisteCorreoElectronicoAsync(string correoElectronico, int? idExcluir = null);
    }
}