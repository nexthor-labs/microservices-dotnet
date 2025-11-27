using System;
using eCommerce.Core;
using eCommerce.Core.Interfaces.Services;

namespace eCommerce.Infraestructure.Services;

public class ProductsService : IProductsService
{
    private readonly HttpClient _httpClient;

    public ProductsService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient(OrdersServiceConstants.ProductsHttpClient);
    }

    public async Task<bool> IsProductAvailableAsync(string productId, int quantity)
    {
        var response = await _httpClient.GetAsync($"/products/{productId}/availability?quantity={quantity}");
        return response.IsSuccessStatusCode;
    }
}
