using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eCommerce.Core.Entities;
using eCommerce.Core.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace eCommerce.Infraestructure.Services;

public class JwtTokenGenerator
{
    private readonly JwtOption _jwtOption;
    public JwtTokenGenerator(IOptions<JwtOption> jwtOption)
    {
        _jwtOption = jwtOption.Value ?? throw new ArgumentNullException(nameof(jwtOption));
    }

    public (string Token, DateTime expires) GenerateToken(ApplicationUser user)
    {
        var key = Encoding.UTF8.GetBytes(_jwtOption.Key ?? throw new ArgumentNullException("JWT Key is null"));
        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var expires = DateTime.UtcNow.AddMinutes(_jwtOption.ExpiryInMinutes);
        var token = new JwtSecurityToken(
            issuer: _jwtOption.Issuer,
            audience: _jwtOption.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: credentials
        );
        return (new JwtSecurityTokenHandler().WriteToken(token), expires);
    }
}
