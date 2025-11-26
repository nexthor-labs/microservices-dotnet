using eCommerce.Core.DTOs;
using eCommerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _service;
    public ProductsController(IProductsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken = default)
    {
        var products = await _service.GetAllProductsAsync(cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _service.GetProductByIdAsync(id, cancellationToken);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductRequest productRequest, CancellationToken cancellationToken = default)
    {
        var product = await _service.AddProductAsync(productRequest, cancellationToken);
        return CreatedAtAction(nameof(GetProductById), new { id = product.ProductID }, productRequest);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductRequest productRequest, CancellationToken cancellationToken = default)
    {
        await _service.UpdateProductAsync(id, productRequest, cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id}/stock")]
    public async Task<IActionResult> UpdateProductStock(Guid id, [FromBody] ProductUpdateStockRequest productRequest, CancellationToken cancellationToken = default)
    {
        await _service.UpdateProductStockAsync(id, productRequest, cancellationToken);
        return NoContent();
    }
}
