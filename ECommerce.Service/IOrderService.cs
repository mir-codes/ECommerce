using System.Threading;
using System.Threading.Tasks;
using ECommerce.Service.Models;

namespace ECommerce.Service
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);
        Task<OrderDto> GetByIdAsync(int orderId, CancellationToken cancellationToken = default);
        Task<PagedResult<OrderDto>> GetHistoryAsync(int customerId, int page, int pageSize, CancellationToken cancellationToken = default);
    }
}
