using System;
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

    public async Task AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Guid productId)
    {
        var product = await GetProductByIdAsync(productId);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }

    public async Task<Product?> GetProductByIdAsync(Guid productId)
    {
        var product = await _context.Products.FindAsync(productId);
        return product;
    }

    public async Task UpdateProductAsync(Product product)
    {
        var item = await GetProductByIdAsync(product.ProductID);
        if (item != null)
        {
            item.ProductName = product.ProductName;
            item.Category = product.Category;
            item.UnitPrice = product.UnitPrice;
            item.QuantityInStock = product.QuantityInStock;

            await _context.SaveChangesAsync();
        }
    }
}
