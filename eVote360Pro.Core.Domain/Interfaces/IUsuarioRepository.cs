using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario?> LoginAsync(string nombreUsuario, string contrasena);

        Task<int> CountAdministradoresActivosAsync();
    }
}