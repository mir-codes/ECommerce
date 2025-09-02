using ECommerce.Domain.Entities;
using ECommerce.Service;
using ECommerce.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ECommerce.Test.Unit
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _serviceMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _serviceMock = new Mock<IProductService>();
            _controller = new ProductController(_serviceMock.Object);
        }

        [Fact]
        public void GetAll_ReturnsProducts()
        {
            var products = new List<Product> { new Product { Id = 1, ProductName = "Test", UnitPrice = 10 } };
            _serviceMock.Setup(s => s.GetAll()).Returns(products);
            var result = _controller.GetAll();
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(products, okResult.Value);
        }

        [Fact]
        public void GetById_ReturnsProduct_WhenExists()
        {
            var product = new Product { Id = 1, ProductName = "Test", UnitPrice = 10 };
            _serviceMock.Setup(s => s.GetById(1)).Returns(product);
            var result = _controller.GetById(1);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(product, okResult.Value);
        }

        [Fact]
        public void GetById_ReturnsNotFound_WhenNotExists()
        {
            _serviceMock.Setup(s => s.GetById(2)).Returns((Product)null);
            var result = _controller.GetById(2);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Add_ReturnsCreatedAtAction()
        {
            var product = new Product { Id = 1, ProductName = "Test", UnitPrice = 10 };
            var result = _controller.Add(product);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(product, createdResult.Value);
        }

        [Fact]
        public void Update_ReturnsNoContent_WhenIdMatches()
        {
            var product = new Product { Id = 1, ProductName = "Test", UnitPrice = 10 };
            var result = _controller.Update(1, product);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Update_ReturnsBadRequest_WhenIdMismatch()
        {
            var product = new Product { Id = 2, ProductName = "Test", UnitPrice = 10 };
            var result = _controller.Update(1, product);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNoContent()
        {
            var result = _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
