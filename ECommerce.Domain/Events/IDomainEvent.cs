using System;

namespace ECommerce.Domain.Events
{
    public interface IDomainEvent
    {
        DateTimeOffset OccurredOn { get; }
    }
}
