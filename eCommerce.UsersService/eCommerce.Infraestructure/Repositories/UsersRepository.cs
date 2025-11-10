using System;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces.Repositories;

namespace eCommerce.Infraestructure.Repositories;

public class UsersRepository : IUsersRepository
{
    public async Task<ApplicationUser> AddUser(ApplicationUser user)
    {
        await Task.CompletedTask;

        return user;
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
        await Task.CompletedTask;

        return new ApplicationUser
        {
            PersonName = "First User",
            Email = email,
            Gender = GenderOptions.Male.ToString()
        };
    }
}
