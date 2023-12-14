using LinguaNex.Domain;
using LinguaNex.EntityFrameworkCore;

namespace LinguaNex.Uow
{

    public class UnitOfWork(LinguaNexDbContext dbContext) : IUnitOfWork
    {
        private IDbTransaction? Transaction = null;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            Transaction = new DbTransaction(dbContext);
            await Transaction.BeginTransactionAsync(cancellationToken);
            return Transaction;
        }
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (Transaction == null)
            {
                throw new Exception("Transaction is null, Please BeginTransaction");
            }
            await Transaction.CommitAsync(cancellationToken);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (Transaction == null)
            {
                throw new Exception("Transaction is null, Please BeginTransaction");
            }
            await Transaction.RollbackAsync(cancellationToken);
        }
        public void Dispose()
        {
            if (Transaction != null)
                Transaction.Dispose();
            dbContext.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            if (Transaction != null)
                await Transaction.DisposeAsync();
            await dbContext.DisposeAsync();
        }
    }
}
