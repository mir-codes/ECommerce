using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enum;
using ECommerce.Domain.Interfaces;
using ECommerce.Service.Models;

namespace ECommerce.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<ProductListItemDto>> GetAllAsync(string keyword, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var skip = (page - 1) * pageSize;
            var items = await _productRepository.SearchAsync(keyword, skip, pageSize, cancellationToken);
            var total = await _productRepository.CountAsync(keyword, cancellationToken);

            return new PagedResult<ProductListItemDto>
            {
                Items = items.Select(p => new ProductListItemDto
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Sku = p.Sku,
                    UnitPrice = p.UnitPrice,
                    CategoryName = p.Category?.Name,
                    VariantCount = p.Variants.Count,
                    IsActive = p.Status == EntityStatus.Active
                }).ToList(),
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<ProductDetailDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdWithDetailsAsync(id, cancellationToken);
            if (product == null)
            {
                return null;
            }

            return new ProductDetailDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Sku = product.Sku,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                CategoryName = product.Category?.Name,
                Variants = product.Variants.Select(v => new ProductVariantDto
                {
                    Id = v.Id,
                    Sku = v.Sku,
                    Color = v.Color,
                    Size = v.Size,
                    PriceDelta = v.PriceDelta,
                    InventoryQuantity = v.InventoryQuantity
                }).ToList(),
                Images = product.Images.Select(i => i.Url).ToList()
            };
        }

        public async Task<int> AddAsync(CreateProductRequest request, CancellationToken cancellationToken = default)
        {
            var product = new Product
            {
                ProductName = request.ProductName,
                Sku = request.Sku,
                Description = request.Description,
                CategoryId = request.CategoryId,
                UnitPrice = request.UnitPrice,
                TrackInventory = request.TrackInventory,
                ReorderPoint = request.ReorderPoint
            };

            await _productRepository.AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return product.Id;
        }

        public async Task UpdateAsync(UpdateProductRequest request, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            product.ProductName = request.ProductName;
            product.Sku = request.Sku;
            product.Description = request.Description;
            product.CategoryId = request.CategoryId;
            product.UnitPrice = request.UnitPrice;
            product.TrackInventory = request.TrackInventory;
            product.ReorderPoint = request.ReorderPoint;
            product.UpdatedAt = DateTimeOffset.UtcNow;

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken);
            if (product == null)
            {
                return;
            }

            product.IsDeleted = true;
            product.Status = EntityStatus.Archived;
            product.UpdatedAt = DateTimeOffset.UtcNow;
            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
