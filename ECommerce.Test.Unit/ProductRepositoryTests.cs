using ECommerce.Domain.Entities;
using ECommerce.Persistence;
using Xunit;
using System.Linq;

namespace ECommerce.Test.Unit
{
    public class ProductRepositoryTests
    {
        private IProductRepository _repo = new ProductRepository();

        [Fact]
        public void AddProduct_ShouldIncreaseCount()
        {
            var product = new Product { Id = 1, ProductName = "Test", UnitPrice = 10 };
            _repo.Add(product);
            Assert.Equal(1, _repo.GetAll().Count());
        }

        [Fact]
        public void GetById_ReturnsProduct_WhenExists()
        {
            var product = new Product { Id = 2, ProductName = "Test2", UnitPrice = 20 };
            _repo.Add(product);
            var result = _repo.GetById(2);
            Assert.NotNull(result);
            Assert.Equal("Test2", result.ProductName);
        }

        [Fact]
        public void GetById_ReturnsNull_WhenNotExists()
        {
            var result = _repo.GetById(999);
            Assert.Null(result);
        }

        [Fact]
        public void UpdateProduct_ChangesValues()
        {
            var product = new Product { Id = 3, ProductName = "Old", UnitPrice = 5 };
            _repo.Add(product);
            product.ProductName = "New";
            product.UnitPrice = 15;
            _repo.Update(product);
            var updated = _repo.GetById(3);
            Assert.Equal("New", updated.ProductName);
            Assert.Equal(15, updated.UnitPrice);
        }

        [Fact]
        public void DeleteProduct_RemovesProduct()
        {
            var product = new Product { Id = 4, ProductName = "DeleteMe", UnitPrice = 1 };
            _repo.Add(product);
            _repo.Delete(4);
            Assert.Null(_repo.GetById(4));
        }
    }
}
