using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence
{
    public class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(ECommerceDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Product>> SearchAsync(string keyword, int skip, int take, CancellationToken cancellationToken = default)
        {
            var query = DbContext.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(p => p.ProductName.Contains(keyword) || p.Sku.Contains(keyword));
            }

            return await query
                .Include(p => p.Category)
                .Include(p => p.Variants)
                .Include(p => p.Images)
                .OrderBy(p => p.ProductName)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public Task<Product> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default)
        {
            return DbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Variants)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public Task<int> CountAsync(string keyword, CancellationToken cancellationToken = default)
        {
            var query = DbContext.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(p => p.ProductName.Contains(keyword) || p.Sku.Contains(keyword));
            }

            return query.CountAsync(cancellationToken);
        }
    }
}
