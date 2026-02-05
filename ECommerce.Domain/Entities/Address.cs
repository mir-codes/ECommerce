namespace ECommerce.Domain.Entities
{
    public class Address : BaseEntity
    {
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public string Line1 { get; set; } = string.Empty;

        public string? Line2 { get; set; }

        public string City { get; set; } = string.Empty;

        public string? Region { get; set; }

        public string PostalCode { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public bool IsDefaultShipping { get; set; }

        public bool IsDefaultBilling { get; set; }
    }
}
