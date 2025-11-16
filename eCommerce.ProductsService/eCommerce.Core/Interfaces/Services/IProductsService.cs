using System;
using eCommerce.Core.DTOs;

namespace eCommerce.Core.Interfaces.Services;

public interface IProductsService
{
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
    Task<ProductResponse?> GetProductByIdAsync(Guid productId);
    Task AddProductAsync(ProductRequest productRequest);
    Task UpdateProductAsync(Guid productId, ProductRequest productRequest);
    Task DeleteProductAsync(Guid productId);
}
