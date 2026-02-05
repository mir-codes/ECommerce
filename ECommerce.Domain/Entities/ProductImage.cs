namespace ECommerce.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string FileName { get; set; }

        public string Url { get; set; }

        public bool IsPrimary { get; set; }
    }
}
