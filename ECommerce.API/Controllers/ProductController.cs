using ECommerce.Service;
using ECommerce.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductListItemDto>>> GetAll(
            [FromQuery] string keyword,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var products = await _productService.GetAllAsync(keyword, page, pageSize, cancellationToken);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var product = await _productService.GetByIdAsync(id, cancellationToken);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductRequest request, CancellationToken cancellationToken = default)
        {
            var id = await _productService.AddAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequest request, CancellationToken cancellationToken = default)
        {
            if (id != request.Id) return BadRequest();
            await _productService.UpdateAsync(request, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _productService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
