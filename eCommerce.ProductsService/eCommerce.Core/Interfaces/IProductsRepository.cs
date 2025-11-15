using System;
using eCommerce.Core.Entities;

namespace eCommerce.Core.Interfaces;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid productId);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Guid productId);
}
