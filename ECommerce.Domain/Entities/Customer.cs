using System.Collections.Generic;

namespace ECommerce.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string CustomerName { get; set; } = string.Empty;
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Fax { get; set; }

        public List<Address> Addresses { get; set; } = new();

        public List<Order> Orders { get; set; } = new();
    }
}
