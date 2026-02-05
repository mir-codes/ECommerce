using System;

namespace ECommerce.Domain.Entities
{
    public class RecentlyViewedItem : BaseEntity
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTimeOffset LastViewedAt { get; set; } = DateTimeOffset.UtcNow;

        public Customer Customer { get; set; }
        public Product Product { get; set; }
    }
}
