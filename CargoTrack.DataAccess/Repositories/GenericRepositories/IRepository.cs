using CargoTrack.Entity.Entities.Common;

namespace CargoTrack.DataAccess.Repositories.GenericRepositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        // CREATE
        Task CreateAsync(TEntity entity);

        // READ
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetAllAsync();

        // UPDATE
        Task UpdateAsync(TEntity entity);

        // DELETE
        Task<bool> DeleteAsync(TEntity entity);
    }
}