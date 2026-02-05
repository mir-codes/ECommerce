using System;

namespace ECommerce.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public string Provider { get; set; }
        public string ProviderPaymentId { get; set; }
        public string IdempotencyKey { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ConfirmedAtUtc { get; set; }
        public string FailureReason { get; set; }
    }
}
