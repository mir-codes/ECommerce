namespace ECommerce.Domain.Entities
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; } = string.Empty;

        public decimal PercentageOff { get; set; }

        public decimal? AmountOff { get; set; }

        public DateTime StartsAtUtc { get; set; }

        public DateTime? EndsAtUtc { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
