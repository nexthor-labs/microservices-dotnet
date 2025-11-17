using System;
using System.Linq.Expressions;
using eCommerce.Core.Entities;

namespace eCommerce.Core.Interfaces.Repositories;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetProductsByCondition(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken = default);
    Task<Product?> GetProductByContitionAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken = default);
    Task AddProductAsync(Product product, CancellationToken cancellationToken = default);
    Task UpdateProductAsync(Product product, CancellationToken cancellationToken = default);
    Task DeleteProductAsync(Guid productId, CancellationToken cancellationToken = default);
    Task <IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);
}
