using eVote360Pro.Core.Domain.Entities;

namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface ICiudadanoRepository : IGenericRepository<Ciudadano>
    {
        Task<bool> ExisteDocumentoAsync(string documento);
        Task<bool> ExisteCorreoAsync(string correo);
        Task<Ciudadano?> ObtenerPorDocumentoAsync(string documento);
    }
}
