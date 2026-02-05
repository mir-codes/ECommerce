using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain;
using ECommerce.Domain.Entities;
using ECommerce.Service.Payments;

namespace ECommerce.Infrastructure
{
    public class StripePaymentService : IPaymentService
    {
        public Task<Payment> CreatePaymentIntentAsync(Order order, string idempotencyKey, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                OrderId = order.Id,
                Amount = order.GrandTotal,
                Currency = order.Currency,
                Provider = "Stripe",
                IdempotencyKey = idempotencyKey,
                ProviderPaymentId = $"pi_{order.Id}_{idempotencyKey}"
            };

            return Task.FromResult(payment);
        }

        public Task<Payment> ConfirmPaymentAsync(string providerPaymentId, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                Provider = "Stripe",
                ProviderPaymentId = providerPaymentId,
                Status = PaymentStatus.Succeeded
            };

            return Task.FromResult(payment);
        }
    }
}
