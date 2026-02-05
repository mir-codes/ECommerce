using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;

namespace ECommerce.Service.Payments
{
    public interface IPaymentService
    {
        Task<Payment> CreatePaymentIntentAsync(Order order, string idempotencyKey, CancellationToken cancellationToken);
        Task<Payment> ConfirmPaymentAsync(string providerPaymentId, CancellationToken cancellationToken);
    }
}
