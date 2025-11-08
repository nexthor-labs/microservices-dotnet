using System;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces.Repositories;

namespace eCommerce.Infraestructure.Repositories;

public class UsersRepository : IUsersRepository
{
    public async Task<ApplicationUser> AddUser(ApplicationUser user)
    {
        user.UserID = Guid.NewGuid();

        await Task.CompletedTask;

        return user;
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
        await Task.CompletedTask;

        return new ApplicationUser
        {
            UserID = Guid.NewGuid(),
            PersonName = "First User",
            Email = email,
            Password = password,
            Gender = GenderOptions.Male.ToString()
        };
    }
}
