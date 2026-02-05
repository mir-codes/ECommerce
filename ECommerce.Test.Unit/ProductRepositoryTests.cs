using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ECommerce.Test.Unit
{
    public class ProductRepositoryTests
    {
        private static ECommerceDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                .UseInMemoryDatabase(databaseName: $"ProductRepoTests_{System.Guid.NewGuid()}")
                .Options;

            return new ECommerceDbContext(options);
        }

        [Fact]
        public async Task AddProduct_ShouldIncreaseCount()
        {
            await using var dbContext = CreateDbContext();
            var repo = new ProductRepository(dbContext);

            var product = new Product { ProductName = "Test", UnitPrice = 10, Sku = "SKU-1" };
            await repo.AddAsync(product);
            await dbContext.SaveChangesAsync();

            var items = await repo.ListAsync();
            Assert.Single(items);
        }

        [Fact]
        public async Task GetById_ReturnsProduct_WhenExists()
        {
            await using var dbContext = CreateDbContext();
            var repo = new ProductRepository(dbContext);

            var product = new Product { ProductName = "Test2", UnitPrice = 20, Sku = "SKU-2" };
            await repo.AddAsync(product);
            await dbContext.SaveChangesAsync();

            var result = await repo.GetByIdAsync(product.Id);
            Assert.NotNull(result);
            Assert.Equal("Test2", result.ProductName);
        }

        [Fact]
        public async Task GetById_ReturnsNull_WhenNotExists()
        {
            await using var dbContext = CreateDbContext();
            var repo = new ProductRepository(dbContext);

            var result = await repo.GetByIdAsync(999);
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateProduct_ChangesValues()
        {
            await using var dbContext = CreateDbContext();
            var repo = new ProductRepository(dbContext);

            var product = new Product { ProductName = "Old", UnitPrice = 5, Sku = "SKU-3" };
            await repo.AddAsync(product);
            await dbContext.SaveChangesAsync();

            product.ProductName = "New";
            product.UnitPrice = 15;
            repo.Update(product);
            await dbContext.SaveChangesAsync();

            var updated = await repo.GetByIdAsync(product.Id);
            Assert.Equal("New", updated.ProductName);
            Assert.Equal(15, updated.UnitPrice);
        }

        [Fact]
        public async Task DeleteProduct_RemovesProduct()
        {
            await using var dbContext = CreateDbContext();
            var repo = new ProductRepository(dbContext);

            var product = new Product { ProductName = "DeleteMe", UnitPrice = 1, Sku = "SKU-4" };
            await repo.AddAsync(product);
            await dbContext.SaveChangesAsync();

            repo.Remove(product);
            await dbContext.SaveChangesAsync();

            Assert.Null(await repo.GetByIdAsync(product.Id));
        }
    }
}
