using LinguaNex.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LinguaNex.Uow
{
    public class DbTransaction(DbContext dbContext) : IDbTransaction
    {
        IDbContextTransaction? CurrentDbContextTransaction;

        bool isCommit = false;
        bool isRollback = false;

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            CurrentDbContextTransaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync();
            await dbContext.Database.CommitTransactionAsync();
            isCommit = true;
            CurrentDbContextTransaction = null;
        }
        public void Commit()
        {
            dbContext.Database.CommitTransaction();
            isCommit = true;
            CurrentDbContextTransaction = null;
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.Database.RollbackTransactionAsync(cancellationToken);
            isRollback = true;
            CurrentDbContextTransaction = null;
        }
        public void Dispose()
        {
            if (CurrentDbContextTransaction != null)
            {
                if (!isCommit && !isRollback)
                {
                    Commit();
                }
                CurrentDbContextTransaction.Dispose();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (CurrentDbContextTransaction != null)
            {
                if (!isCommit && !isRollback)
                {
                    await CommitAsync();
                }
                await CurrentDbContextTransaction.DisposeAsync();
            }
        }

    }
}
