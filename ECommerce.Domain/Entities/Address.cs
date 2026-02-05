namespace ECommerce.Domain.Entities
{
    public class Address : BaseEntity
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public string Label { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public bool IsDefault { get; set; }
    }
}
