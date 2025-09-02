using ECommerce.Domain.Entities;
using System.Collections.Generic;

namespace ECommerce.Service
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
