namespace ECommerce.Domain.Entities
{
    public class ProductReview : BaseEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }
    }
}
