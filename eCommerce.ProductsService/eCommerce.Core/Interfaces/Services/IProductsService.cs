using System;
using eCommerce.Core.DTOs;

namespace eCommerce.Core.Interfaces.Services;

public interface IProductsService
{
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync(CancellationToken cancellationToken = default);
    Task<ProductResponse?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<ProductResponse> AddProductAsync(ProductRequest productRequest, CancellationToken cancellationToken = default);
    Task UpdateProductAsync(Guid productId, ProductRequest productRequest, CancellationToken cancellationToken = default);
    Task DeleteProductAsync(Guid productId, CancellationToken cancellationToken = default);
    Task UpdateProductStockAsync(Guid productId, ProductUpdateStockRequest productRequest, CancellationToken cancellationToken = default);
}
