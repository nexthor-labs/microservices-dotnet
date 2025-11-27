using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Infraestructure.Handlers;

public class BearerTokenDelegatingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BearerTokenDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        // Extract the bearer token from the incoming request
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
        
        // If token exists, add it to the outgoing request
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = AuthenticationHeaderValue.Parse(token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
