using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
