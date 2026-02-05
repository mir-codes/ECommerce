using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _dbContext;

        public ProductRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .Include(product => product.Category)
                .Include(product => product.Variants)
                .Include(product => product.Images)
                .Where(product => !product.IsDeleted)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .Include(product => product.Category)
                .Include(product => product.Variants)
                .Include(product => product.Images)
                .FirstOrDefaultAsync(product => product.Id == id && !product.IsDeleted, cancellationToken);
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task SoftDeleteAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            if (product == null)
            {
                return;
            }

            product.IsDeleted = true;
            product.Status = EntityStatus.Inactive;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
