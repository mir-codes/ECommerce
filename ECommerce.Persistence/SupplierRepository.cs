using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence
{
    public class SupplierRepository : EfRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ECommerceDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<Supplier> GetByNameAsync(string name, CancellationToken cancellationToken = default)
            => DbContext.Suppliers.FirstOrDefaultAsync(s => s.Name == name, cancellationToken);
    }
}
