using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Persistence;

namespace ECommerce.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken) =>
            _productRepository.GetAllAsync(cancellationToken);

        public Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken) =>
            _productRepository.GetByIdAsync(id, cancellationToken);

        public Task AddAsync(Product product, CancellationToken cancellationToken) =>
            _productRepository.AddAsync(product, cancellationToken);

        public Task UpdateAsync(Product product, CancellationToken cancellationToken) =>
            _productRepository.UpdateAsync(product, cancellationToken);

        public Task SoftDeleteAsync(int id, CancellationToken cancellationToken) =>
            _productRepository.SoftDeleteAsync(id, cancellationToken);
    }
}
