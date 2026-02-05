using System.Collections.Generic;

namespace ECommerce.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string UserId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<Address> Addresses { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public List<WishlistItem> WishlistItems { get; set; } = new();
        public List<RecentlyViewedProduct> RecentlyViewedProducts { get; set; } = new();
    }
}
