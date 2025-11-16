using System;
using eCommerce.Core.DTOs;
using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Core.Interfaces.Services;

namespace eCommerce.Infraestructure.Services;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _repository;

    public ProductsService(IProductsRepository repository)
    {
        _repository = repository;
    }
    
    public Task AddProductAsync(ProductRequest productRequest)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse?> GetProductByIdAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProductAsync(Guid productId, ProductRequest productRequest)
    {
        throw new NotImplementedException();
    }
}
