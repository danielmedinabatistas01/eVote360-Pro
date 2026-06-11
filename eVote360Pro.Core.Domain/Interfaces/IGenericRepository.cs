namespace eVote360Pro.Core.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> AddAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(int id, TEntity entity);
        Task DeleteAsync(int id);
        Task<List<TEntity>> GetAllList();
        Task<TEntity?> GetById(int id);
        IQueryable<TEntity> GetAllQuery();
        IQueryable<TEntity> GetAllQueryWithInclude(List<string> properties);
    }
}