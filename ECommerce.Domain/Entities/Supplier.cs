using System.Collections.Generic;

namespace ECommerce.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string SupplierName { get; set; }

        public string ContactEmail { get; set; }

        public string Phone { get; set; }

        public bool IsApproved { get; set; }

        public List<SupplierProduct> SupplierProducts { get; set; } = new();
    }
}
