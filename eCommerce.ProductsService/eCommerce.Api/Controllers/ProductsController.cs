using eCommerce.Core.DTOs;
using eCommerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _service;
    public ProductsController(IProductsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _service.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _service.GetProductByIdAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductRequest productRequest)
    {
        var product = await _service.AddProductAsync(productRequest);
        return CreatedAtAction(nameof(GetProductById), new { id = product.ProductID }, productRequest);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductRequest productRequest)
    {
        await _service.UpdateProductAsync(id, productRequest);
        return NoContent();
    }

    [HttpPatch("{id}/stock")]
    public async Task<IActionResult> UpdateProductStock(Guid id, [FromBody] ProductUpdateStockRequest productRequest)
    {
        await _service.UpdateProductStockAsync(id, productRequest);
        return NoContent();
    }
}
