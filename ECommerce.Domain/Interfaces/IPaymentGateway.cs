using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Models;

namespace ECommerce.Domain.Interfaces
{
    public interface IPaymentGateway
    {
        Task<PaymentCheckoutSession> CreateCheckoutSessionAsync(PaymentCheckoutRequest request, CancellationToken cancellationToken = default);
        Task<bool> ConfirmPaymentAsync(PaymentConfirmation request, CancellationToken cancellationToken = default);
    }
}
