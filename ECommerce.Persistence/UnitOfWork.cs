using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECommerceDbContext _dbContext;

        public UnitOfWork(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _dbContext.SaveChangesAsync(cancellationToken);
    }
}
