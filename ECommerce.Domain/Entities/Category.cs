using System.Collections.Generic;

namespace ECommerce.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public List<Product> Products { get; set; } = new();
    }
}
