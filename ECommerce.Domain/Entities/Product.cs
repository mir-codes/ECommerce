using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; } = string.Empty;

        public string Sku { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ProductInventory Inventory { get; set; } = new();

        public List<ProductVariant> Variants { get; set; } = new();

        public List<ProductImage> Images { get; set; } = new();

        public ProductVisibility Visibility { get; set; } = ProductVisibility.Public;

    }
}
