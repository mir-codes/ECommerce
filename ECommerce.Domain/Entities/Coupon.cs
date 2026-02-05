using System;

namespace ECommerce.Domain.Entities
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime? StartsAt { get; set; }
        public DateTime? EndsAt { get; set; }
        public int? MaxRedemptions { get; set; }
        public int RedemptionCount { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
