using System.ComponentModel.DataAnnotations;
using ECommerce.Domain.Events;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAtUtc { get; set; }

        public bool IsDeleted { get; set; }

        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public List<IDomainEvent> DomainEvents { get; } = new();
    }
}
