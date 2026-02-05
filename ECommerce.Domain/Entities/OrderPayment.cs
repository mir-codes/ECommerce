using ECommerce.Domain.ValueObjects;

namespace ECommerce.Domain.Entities
{
    public class OrderPayment : BaseEntity
    {
        public int OrderId { get; set; }

        public Order? Order { get; set; }

        public string Provider { get; set; } = "Stripe";

        public string ProviderPaymentId { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "USD";

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public string? FailureReason { get; set; }
    }
}
