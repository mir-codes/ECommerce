using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Domain.Enum;

namespace ECommerce.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }

        public string Sku { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public string Description { get; set; }

        public ProductStatus Status { get; set; } = ProductStatus.Draft;

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();

        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        public ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();
    }
}
