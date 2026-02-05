using System.Threading;
using System.Threading.Tasks;
using ECommerce.Service.Models;

namespace ECommerce.Service
{
    public interface ICheckoutService
    {
        Task<CheckoutSessionDto> CreateCheckoutAsync(CreateCheckoutRequest request, CancellationToken cancellationToken = default);
        Task<bool> ConfirmPaymentAsync(ConfirmPaymentRequest request, CancellationToken cancellationToken = default);
    }
}
