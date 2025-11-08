using System;
using eCommerce.Core.Entities;

namespace eCommerce.Core.Interfaces.Repositories;

public interface IUsersRepository
{
    Task<ApplicationUser> AddUser(ApplicationUser user);
    Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password);
}
