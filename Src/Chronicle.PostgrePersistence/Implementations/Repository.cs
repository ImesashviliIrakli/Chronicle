using Chronicle.Domain.Primitives;
using Chronicle.Domain.Repositories;
using Chronicle.PostgrePersistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Chronicle.PostgrePersistence.Implementations;

public class Repository<TEntity>(AppDbContext context) : IRepository<TEntity> where TEntity : Entity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    #region Read

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<List<TEntity>> GetByUserIdAsync(Guid userId)
    {
        return await _dbSet.Where(x => x.UserId.Equals(userId)).ToListAsync();
    }

    #endregion

    #region Write
    public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

    public void Delete(TEntity entity) => _dbSet.Remove(entity);
    
    #endregion
}
