using System;
using System.Linq;
using System.Linq.Expressions;
using ECommerce.Domain;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
        public DbSet<ProductImage> ProductImages => Set<ProductImage>();
        public DbSet<ProductReview> ProductReviews => Set<ProductReview>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<SupplierProduct> SupplierProducts => Set<SupplierProduct>();
        public DbSet<Coupon> Coupons => Set<Coupon>();
        public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();
        public DbSet<RecentlyViewedItem> RecentlyViewedItems => Set<RecentlyViewedItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplySoftDeleteFilter(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Sku)
                .IsUnique();

            modelBuilder.Entity<ProductVariant>()
                .HasIndex(p => p.Sku)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();

            modelBuilder.Entity<WishlistItem>()
                .HasIndex(w => new { w.CustomerId, w.ProductId })
                .IsUnique();

            modelBuilder.Entity<RecentlyViewedItem>()
                .HasIndex(r => new { r.CustomerId, r.ProductId })
                .IsUnique();
        }

        private static void ApplySoftDeleteFilter(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "entity");
                    var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                    var compare = Expression.Equal(property, Expression.Constant(false));
                    var lambda = Expression.Lambda(compare, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }
    }
}
