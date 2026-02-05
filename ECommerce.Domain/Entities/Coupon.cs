using System;

namespace ECommerce.Domain.Entities
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; }

        public decimal Percentage { get; set; }

        public decimal? MaxDiscountAmount { get; set; }

        public DateTime StartsAt { get; set; }

        public DateTime? EndsAt { get; set; }

        public bool IsActive { get; set; }
    }
}
