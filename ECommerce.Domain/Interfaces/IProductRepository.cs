using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Product>> SearchAsync(string keyword, int skip, int take, CancellationToken cancellationToken = default);
        Task<int> CountAsync(string keyword, CancellationToken cancellationToken = default);
    }
}
