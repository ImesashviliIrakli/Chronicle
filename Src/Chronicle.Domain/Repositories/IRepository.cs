using Chronicle.Domain.Primitives;

namespace Chronicle.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<List<TEntity>> GetByUserIdAsync(Guid userId);
    Task<TEntity> GetByIdAsync(int id);
    Task AddAsync(TEntity entity);
    void Delete(TEntity entity);
}
