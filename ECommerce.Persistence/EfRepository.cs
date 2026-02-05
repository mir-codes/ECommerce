using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence
{
    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly ECommerceDbContext DbContext;

        public EfRepository(ECommerceDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => DbContext.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken).AsTask();

        public async Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => await DbContext.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);

        public async Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken cancellationToken = default)
            => await DbContext.Set<TEntity>().ToListAsync(cancellationToken);

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
            => await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        public void Update(TEntity entity)
            => DbContext.Set<TEntity>().Update(entity);

        public void Remove(TEntity entity)
            => DbContext.Set<TEntity>().Remove(entity);
    }
}
