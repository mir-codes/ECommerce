using System.Collections.Generic;

namespace ECommerce.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string SupplierName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public List<SupplierProduct> SupplierProducts { get; set; } = new();
    }
}
