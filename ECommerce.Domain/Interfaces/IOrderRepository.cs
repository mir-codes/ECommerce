using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetByIdWithItemsAsync(int orderId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Order>> GetByCustomerAsync(int customerId, int skip, int take, CancellationToken cancellationToken = default);
    }
}
