using System;

namespace eCommerce.Core.DTOs;

public class RegisterRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PersonName { get; set; }
    public string? Gender { get; set; }
}
