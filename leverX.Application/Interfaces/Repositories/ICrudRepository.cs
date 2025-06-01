namespace leverX.Application.Interfaces.Repositories
{
    public interface ICrudRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }

}
