using System;

namespace eCommerce.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = 500,
                ex.Message,
                Type = ex.GetType().Name
            });
        }
    }
}

public static class ExceptionHandlingMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
        // Middleware logic would go here
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }
}
