using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Domain.Enum;

namespace ECommerce.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public string Provider { get; set; }
        public string ProviderReference { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public string IdempotencyKey { get; set; }

        public Order Order { get; set; }
    }
}
