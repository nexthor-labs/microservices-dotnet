using System;

namespace eCommerce.Core.Interfaces.Services;

public interface IProductsService
{
    Task<bool> IsProductAvailableAsync(string productId, int quantity);
}
