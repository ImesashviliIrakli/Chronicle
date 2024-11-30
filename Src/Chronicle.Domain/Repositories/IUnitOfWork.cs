using System.Data;

namespace Chronicle.Domain.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    IDbTransaction BeginTransaction();
}

public interface IAuthUnitOfWork : IUnitOfWork
{
}
