namespace ECommerce.Domain.Settings
{
    public class CacheSettings
    {
        public string RedisConnectionString { get; set; }
        public int DefaultTtlSeconds { get; set; } = 300;
    }
}
