using System.Text.Json.Serialization;
using eCommerce.Infraestructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.OpenApi;
using eCommerce.Core.Options;
using eCommerce.Core;
using eCommerce.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var environment = builder.Environment;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
services.AddOpenApi();

services.AddInfrastructure(configuration);

services.AddInfrastructureDbContext(configuration);

services.AddInfrastructureValidators();

var apiSettings = configuration.GetSection("Api:AllowedHosts").Get<string[]>();

if (apiSettings is not null)
{
    Console.WriteLine("CORS Origins:", string.Join(", ", apiSettings));
    services.AddCors(options =>
    {
        options.AddPolicy(ProductsServiceConstants.CorsPolicy, builder =>
            builder.WithOrigins(apiSettings!)
            .AllowAnyMethod()
            .AllowAnyHeader());
    });
}

services.AddCors();

services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        var optionsJwt = configuration.GetSection("Jwt").Get<JwtOption>() ?? throw new InvalidOperationException("No JWT options found");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = optionsJwt.Issuer,
            ValidAudience = optionsJwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(optionsJwt.Key ?? throw new ArgumentNullException("JWT Key is null"))),
            ClockSkew = TimeSpan.Zero
        };
    });

services.AddAuthorization();

services.AddEndpointsApiExplorer();

services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: 'Bearer {Generated Jwt Token}'",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseExceptionHandling();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
app.UseSwaggerUI();

app.UseCors(ProductsServiceConstants.CorsPolicy);

app.MapControllers();

app.Run();
