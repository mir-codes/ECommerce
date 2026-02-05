using System;
using ECommerce.Domain.Enum;

namespace ECommerce.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }

        public Order Order { get; set; }

        public string Provider { get; set; }

        public string ProviderPaymentId { get; set; }

        public string IdempotencyKey { get; set; }

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "USD";

        public DateTime? PaidAt { get; set; }
    }
}
