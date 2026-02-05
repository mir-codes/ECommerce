namespace ECommerce.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public int ProductId { get; set; }
        public string Url { get; set; }
        public bool IsPrimary { get; set; }

        public Product Product { get; set; }
    }
}
