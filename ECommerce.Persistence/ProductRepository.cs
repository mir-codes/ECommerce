using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Product>> GetAllAsync() =>
            _context.Products
                .Include(product => product.Category)
                .Include(product => product.Images)
                .Include(product => product.Variants)
                .ToListAsync();

        public Task<Product?> GetByIdAsync(int id) =>
            _context.Products
                .Include(product => product.Category)
                .Include(product => product.Images)
                .Include(product => product.Variants)
                .FirstOrDefaultAsync(product => product.Id == id && !product.IsDeleted);

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            product.UpdatedAt = DateTime.UtcNow;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product == null)
            {
                return;
            }

            product.IsDeleted = true;
            product.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
