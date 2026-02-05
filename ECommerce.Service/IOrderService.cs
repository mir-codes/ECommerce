using ECommerce.Domain.Entities;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public interface IOrderService
    {
        Task<Order> CreateOrderFromCartAsync(int customerId);

        Task<Order?> GetByIdAsync(int orderId);

        Task UpdateStatusAsync(int orderId, string status);
    }
}
