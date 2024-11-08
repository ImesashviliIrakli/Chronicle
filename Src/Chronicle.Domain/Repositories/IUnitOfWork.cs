using System.Data;

namespace Chronicle.Domain.Repositories;

public interface IAppUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}

public interface IAuthUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    IDbTransaction BeginTransaction();
}
