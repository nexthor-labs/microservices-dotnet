using System;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public string? PersonName { get; set; }
    public string? Gender { get; set; }
}

public enum GenderOptions
{
    Male,
    Female
}