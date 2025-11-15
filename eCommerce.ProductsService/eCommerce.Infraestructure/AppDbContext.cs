using System;
using eCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infraestructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}
