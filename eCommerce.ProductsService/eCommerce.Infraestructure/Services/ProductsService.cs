using System;
using AutoMapper;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Core.Interfaces.Services;

namespace eCommerce.Infraestructure.Services;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _repository;
    private readonly IMapper _mapper;

    public ProductsService(IProductsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductResponse> AddProductAsync(ProductRequest productRequest, CancellationToken cancellationToken = default)
    {
        var product = _mapper.Map<Product>(productRequest);
        await _repository.AddProductAsync(product, cancellationToken);

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task DeleteProductAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var product = await _repository.GetProductByContitionAsync(x => x.ProductID == productId, cancellationToken) ?? throw new InvalidOperationException("Product not found");
        await _repository.DeleteProductAsync(productId);
    }

    public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await _repository.GetAllProductsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ProductResponse>>(products);
    }

    public async Task<ProductResponse?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var product = await _repository.GetProductByContitionAsync(x => x.ProductID == productId, cancellationToken);
        if (product == null)
            return null;
        return _mapper.Map<ProductResponse>(product);
    }

    public async Task UpdateProductAsync(Guid productId, ProductRequest productRequest, CancellationToken cancellationToken = default)
    {
        var product = await _repository.GetProductByContitionAsync(x => x.ProductID == productId, cancellationToken) ?? throw new InvalidOperationException("Product not found");
        _mapper.Map(productRequest, product);
        await _repository.UpdateProductAsync(product);
    }

    public async Task UpdateProductStockAsync(Guid productId, ProductUpdateStockRequest productRequest, CancellationToken cancellationToken = default)
    {
        var product = await _repository.GetProductByContitionAsync(x => x.ProductID == productId, cancellationToken) ?? throw new InvalidOperationException("Product not found");
        product.QuantityInStock = productRequest.QuantityInStock;
        await _repository.UpdateProductAsync(product);
    }
}
