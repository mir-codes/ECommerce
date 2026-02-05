using System;

namespace ECommerce.Domain.Entities
{
    public class RecentlyViewedProduct : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime ViewedAt { get; set; } = DateTime.UtcNow;
    }
}
