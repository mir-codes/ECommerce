using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly ECommerceDbContext _dbContext;

        public SupplierService(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Supplier>> GetSuppliersAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Suppliers
                .Include(supplier => supplier.SupplierProducts)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public Task<Supplier> GetSupplierAsync(int supplierId, CancellationToken cancellationToken)
        {
            return _dbContext.Suppliers
                .Include(supplier => supplier.SupplierProducts)
                .FirstOrDefaultAsync(supplier => supplier.Id == supplierId, cancellationToken);
        }

        public async Task AddSupplierAsync(Supplier supplier, CancellationToken cancellationToken)
        {
            _dbContext.Suppliers.Add(supplier);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
