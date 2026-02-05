using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Service.Models;

namespace ECommerce.Service
{
    public interface IOrderService
    {
        Task<Order> CheckoutAsync(CheckoutRequest request, CancellationToken cancellationToken);
        Task<Order> GetOrderAsync(int orderId, CancellationToken cancellationToken);
    }
}
