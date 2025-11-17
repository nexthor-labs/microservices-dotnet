using System;
using System.Linq.Expressions;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infraestructure.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly AppDbContext _context;
    public ProductsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProductAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var product = await GetProductByContitionAsync(x => x.ProductID == productId, cancellationToken);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
    public async Task<IEnumerable<Product>> GetProductsByCondition(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var products = await _context.Products.Where(predicate).ToListAsync(cancellationToken);
        return products;
    }

    public async Task<Product?> GetProductByContitionAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FirstOrDefaultAsync(predicate, cancellationToken);
        return product;
    }

    public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        var item = await GetProductByContitionAsync(x => x.ProductID == product.ProductID, cancellationToken);
        if (item != null)
        {
            item.ProductName = product.ProductName;
            item.Category = product.Category;
            item.UnitPrice = product.UnitPrice;
            item.QuantityInStock = product.QuantityInStock;

            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products.ToListAsync(cancellationToken);
    }
}
