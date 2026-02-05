using System.Collections.Generic;

namespace ECommerce.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string Address { get; set; }

        public ICollection<SupplierProduct> SupplierProducts { get; set; } = new List<SupplierProduct>();
    }
}
