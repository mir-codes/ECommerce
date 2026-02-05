using System.Collections.Generic;

namespace ECommerce.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string SupplierName { get; set; } = string.Empty;
        public string? ContactName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
