using eCommerce.Core.DTOs;
using eCommerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrdersService _service;
    private readonly IProductsService _productsService;

    public OrdersController(ILogger<OrdersController> logger, 
    IOrdersService service,
    IProductsService productsService)
    {
        _logger = logger;
        _service = service;
        _productsService = productsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching all orders.");
        var orders = await _service.GetAllOrdersAsync(cancellationToken);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderByIdAsync(string id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching order with ID: {OrderId}", id);
        var order = await _service.GetOrderByIdAsync(Guid.Parse(id), cancellationToken);
        if (order == null)
        {
            _logger.LogWarning("Order with ID: {OrderId} not found.", id);
            return NotFound();
        }
        return Ok(order);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetOrdersByCustomerIdAsync(string customerId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching orders for customer ID: {CustomerId}", customerId);
        var orders = await _service.GetOrdersByUserIdAsync(customerId, cancellationToken);
        return Ok(orders);
    }

    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetOrdersByProductIdAsync(string productId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching orders for product ID: {ProductId}", productId);
        var orders = await _service.GetOrderByProductAsync(Guid.Parse(productId), cancellationToken);
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync([FromBody] OrderAddRequest request, CancellationToken cancellationToken)
    {
        var isAvailable = await _productsService.IsProductAvailableAsync(request.Items.First().ProductId.ToString(), request.Items.First().Quantity);
        if (!isAvailable)
        {
            _logger.LogWarning("Product with ID: {ProductId} is not available in the requested quantity.", request.Items.First().ProductId);
            return BadRequest("Requested product is not available in the desired quantity.");
        }

        _logger.LogInformation("Creating a new order.");
        var order = await _service.AddOrderAsync(request, cancellationToken);
        if (order == null)
        {
            _logger.LogError("Failed to create order.");
            return BadRequest("Could not create order.");
        }
        return CreatedAtAction(nameof(GetOrderByIdAsync), new { id = order.OrderId }, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderAsync(string id, [FromBody] OrderUpdateRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating order with ID: {OrderId}", id);
        request.OrderId = Guid.Parse(id);
        var updatedOrder = await _service.UpdateOrderAsync(request, cancellationToken);
        if (updatedOrder == null)
        {
            _logger.LogWarning("Order with ID: {OrderId} not found for update.", id);
            return NotFound();
        }
        return Ok(updatedOrder);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderAsync(string id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting order with ID: {OrderId}", id);
        await _service.DeleteOrderAsync(Guid.Parse(id), cancellationToken);
        return NoContent();
    }
}
