using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;

namespace ECommerce.Service
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(Product product, CancellationToken cancellationToken);
        Task UpdateAsync(Product product, CancellationToken cancellationToken);
        Task SoftDeleteAsync(int id, CancellationToken cancellationToken);
    }
}
