using eCommerce.Core.DTOs;
using eCommerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUsersService _usersService;
    public AuthController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _usersService.Login(request);
        if (result == null)
            return Unauthorized();
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var registeredUser = await _usersService.Register(request);
        if (registeredUser == null)
            return BadRequest("Registration failed.");

        return Ok(registeredUser);
    }
}

