using System.Collections.Generic;

namespace ECommerce.Service.Models
{
    public class ProductListItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Sku { get; set; }
        public decimal UnitPrice { get; set; }
        public string CategoryName { get; set; }
        public int VariantCount { get; set; }
        public bool IsActive { get; set; }
    }

    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string CategoryName { get; set; }
        public IReadOnlyList<ProductVariantDto> Variants { get; set; }
        public IReadOnlyList<string> Images { get; set; }
    }

    public class ProductVariantDto
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal PriceDelta { get; set; }
        public int InventoryQuantity { get; set; }
    }

    public class CreateProductRequest
    {
        public string ProductName { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public bool TrackInventory { get; set; } = true;
        public int ReorderPoint { get; set; } = 5;
    }

    public class UpdateProductRequest : CreateProductRequest
    {
        public int Id { get; set; }
    }
}
