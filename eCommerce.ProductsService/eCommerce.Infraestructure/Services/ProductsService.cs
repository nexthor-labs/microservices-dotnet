using System;
using AutoMapper;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces.Repositories;
using eCommerce.Core.Interfaces.Services;
using FluentValidation;

namespace eCommerce.Infraestructure.Services;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductRequest> _productValidator;
    private readonly IValidator<ProductUpdateStockRequest> _productStockValidator;

    public ProductsService(IProductsRepository repository, 
    IMapper mapper,
    IValidator<ProductRequest> productValidator,
    IValidator<ProductUpdateStockRequest> productStockValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _productValidator = productValidator;
        _productStockValidator = productStockValidator;
    }

    public async Task<ProductResponse> AddProductAsync(ProductRequest request, CancellationToken cancellationToken = default)
    {
        var validate = await _productValidator.ValidateAsync(request);
        if (!validate.IsValid)
            throw new ValidationException(validate.Errors);

        var product = _mapper.Map<Product>(request);
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

    public async Task UpdateProductAsync(Guid productId, ProductRequest request, CancellationToken cancellationToken = default)
    {
        var validate = await _productValidator.ValidateAsync(request);
        if (!validate.IsValid)
            throw new ValidationException(validate.Errors);

        var product = await _repository.GetProductByContitionAsync(x => x.ProductID == productId, cancellationToken) ?? throw new InvalidOperationException("Product not found");
        _mapper.Map(request, product);
        await _repository.UpdateProductAsync(product);
    }

    public async Task UpdateProductStockAsync(Guid productId, ProductUpdateStockRequest request, CancellationToken cancellationToken = default)
    {
        var validate = await _productStockValidator.ValidateAsync(request);
        if (!validate.IsValid)
            throw new ValidationException(validate.Errors);

       var product = await _repository.GetProductByContitionAsync(x => x.ProductID == productId, cancellationToken) ?? throw new InvalidOperationException("Product not found");
        product.QuantityInStock = request.QuantityInStock;
        await _repository.UpdateProductAsync(product);
    }
}
