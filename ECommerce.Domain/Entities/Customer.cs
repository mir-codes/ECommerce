using System.Collections.Generic;

namespace ECommerce.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
        public ICollection<RecentlyViewedItem> RecentlyViewedItems { get; set; } = new List<RecentlyViewedItem>();
    }
}
