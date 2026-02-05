using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence
{
    public class CustomerRepository : EfRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ECommerceDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<Customer> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
            => DbContext.Customers
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
    }
}
