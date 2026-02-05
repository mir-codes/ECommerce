using ECommerce.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Persistence
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(int id);
    }
}
