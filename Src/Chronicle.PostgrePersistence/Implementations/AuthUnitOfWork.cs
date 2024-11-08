using Chronicle.Domain.Repositories;
using Chronicle.PostgrePersistence.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Chronicle.PostgrePersistence.Implementations;

public class AuthUnitOfWork(AuthDbContext context) : IAuthUnitOfWork
{
    private readonly AuthDbContext _context = context;

    public IDbTransaction BeginTransaction()
    {
        var transaction = _context.Database.BeginTransaction();

        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
}
