using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class SupplierProduct : BaseEntity
    {
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Column(TypeName = "money")]
        public decimal CostPrice { get; set; }

        public int LeadTimeDays { get; set; }
        public int MinimumOrderQuantity { get; set; }
    }
}
