using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public int InventoryQuantity { get; set; }
        public bool TrackInventory { get; set; } = true;
        public List<ProductVariant> Variants { get; set; } = new();
        public List<ProductImage> Images { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
    }
}
