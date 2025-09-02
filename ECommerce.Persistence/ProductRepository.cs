using ECommerce.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> _products = new List<Product>();
        public IEnumerable<Product> GetAll() => _products;
        public Product GetById(int id) => _products.FirstOrDefault(p => p.Id == id);
        public void Add(Product product) => _products.Add(product);
        public void Update(Product product)
        {
            var existing = GetById(product.Id);
            if (existing != null)
            {
                existing.ProductName = product.ProductName;
                existing.UnitPrice = product.UnitPrice;
            }
        }
        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
                _products.Remove(product);
        }
    }
}
