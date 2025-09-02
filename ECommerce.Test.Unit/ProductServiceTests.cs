using ECommerce.Domain.Entities;
using ECommerce.Persistence;
using ECommerce.Service;
using NUnit.Framework;
using System.Linq;

namespace ECommerce.Test.Unit
{
    public class ProductServiceTests
    {
        private IProductRepository _repo;
        private IProductService _service;

        [SetUp]
        public void Setup()
        {
            _repo = new ProductRepository();
            _service = new ProductService(_repo);
        }

        [Test]
        public void AddProduct_ShouldIncreaseCount()
        {
            var product = new Product { Id = 1, ProductName = "Test", UnitPrice = 10 };
            _service.Add(product);
            Assert.AreEqual(1, _service.GetAll().Count());
        }
    }
}
