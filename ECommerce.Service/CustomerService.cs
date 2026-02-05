using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ECommerceDbContext _dbContext;

        public CustomerService(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Customer> GetCustomerAsync(int customerId, CancellationToken cancellationToken)
        {
            return _dbContext.Customers
                .Include(customer => customer.Addresses)
                .Include(customer => customer.WishlistItems)
                .Include(customer => customer.RecentlyViewedProducts)
                .FirstOrDefaultAsync(customer => customer.Id == customerId, cancellationToken);
        }

        public async Task<IReadOnlyList<Order>> GetOrderHistoryAsync(int customerId, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .Include(order => order.OrderDetails)
                .Where(order => order.CustomerId == customerId)
                .OrderByDescending(order => order.OrderDate)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken)
        {
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
