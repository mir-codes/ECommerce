using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence
{
    public class OrderRepository : EfRepository<Order>, IOrderRepository
    {
        public OrderRepository(ECommerceDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Order>> GetByCustomerAsync(int customerId, int skip, int take, CancellationToken cancellationToken = default)
            => await DbContext.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);

        public Task<Order> GetByIdWithItemsAsync(int orderId, CancellationToken cancellationToken = default)
        {
            return DbContext.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        }
    }
}
