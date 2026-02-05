using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Models;

namespace ECommerce.Infrastructure
{
    public class StripePaymentGateway : IPaymentGateway
    {
        public Task<PaymentCheckoutSession> CreateCheckoutSessionAsync(PaymentCheckoutRequest request, CancellationToken cancellationToken = default)
        {
            // TODO: Integrate Stripe SDK with idempotency key and metadata.
            return Task.FromResult(new PaymentCheckoutSession
            {
                SessionId = $"cs_{request.OrderId}",
                CheckoutUrl = "https://checkout.stripe.com/pay/sample"
            });
        }

        public Task<bool> ConfirmPaymentAsync(PaymentConfirmation request, CancellationToken cancellationToken = default)
        {
            // TODO: Validate Stripe webhook signature and reconcile payment intent.
            return Task.FromResult(request.IsSuccess);
        }
    }
}
