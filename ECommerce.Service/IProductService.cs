using System.Threading;
using System.Threading.Tasks;
using ECommerce.Service.Models;

namespace ECommerce.Service
{
    public interface IProductService
    {
        Task<PagedResult<ProductListItemDto>> GetAllAsync(string keyword, int page, int pageSize, CancellationToken cancellationToken = default);
        Task<ProductDetailDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<int> AddAsync(CreateProductRequest request, CancellationToken cancellationToken = default);
        Task UpdateAsync(UpdateProductRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
