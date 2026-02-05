using System;

namespace ECommerce.Domain.Entities
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; }
        public decimal Percentage { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public DateTimeOffset? ExpiresAt { get; set; }
        public int MaxRedemptions { get; set; }
        public int Redemptions { get; set; }
        public bool IsStackable { get; set; }
    }
}
