using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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

        public DbSet<Supplier> Suppliers => Set<Supplier>();

        public DbSet<SupplierProduct> SupplierProducts => Set<SupplierProduct>();

        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();

        public DbSet<Cart> Carts => Set<Cart>();

        public DbSet<CartItem> CartItems => Set<CartItem>();

        public DbSet<Order> Orders => Set<Order>();

        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

        public DbSet<Payment> Payments => Set<Payment>();

        public DbSet<Coupon> Coupons => Set<Coupon>();

        public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(product => product.Sku)
                .IsUnique();

            modelBuilder.Entity<ProductVariant>()
                .HasIndex(variant => variant.Sku)
                .IsUnique();

            modelBuilder.Entity<OrderDetail>()
                .HasIndex(detail => new { detail.OrderId, detail.ProductId })
                .IsUnique();

            modelBuilder.Entity<WishlistItem>()
                .HasIndex(item => new { item.CustomerId, item.ProductId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
