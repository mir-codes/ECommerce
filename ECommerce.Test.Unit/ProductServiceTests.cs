using System.Threading.Tasks;
using ECommerce.Persistence;
using ECommerce.Service;
using ECommerce.Service.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ECommerce.Test.Unit
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task AddProduct_ShouldIncreaseCount()
        {
            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                .UseInMemoryDatabase(databaseName: $"ProductServiceTests_{System.Guid.NewGuid()}")
                .Options;

            await using var dbContext = new ECommerceDbContext(options);
            var repo = new ProductRepository(dbContext);
            var unitOfWork = new UnitOfWork(dbContext);
            var service = new ProductService(repo, unitOfWork);

            var id = await service.AddAsync(new CreateProductRequest
            {
                ProductName = "Test",
                Sku = "SKU-TEST",
                UnitPrice = 10,
                CategoryId = 1
            });

            var result = await service.GetByIdAsync(id);
            Assert.NotNull(result);
            Assert.Equal("Test", result.ProductName);
        }
    }
}
