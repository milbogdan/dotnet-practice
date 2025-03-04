using Microsoft.AspNetCore.Mvc;
using myproject.DAOs;
using myproject.Model;

namespace myproject.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> getAll();
        Task<String> register(RegisterUserDAO registerUserDAO);
        Task<bool> VerifyPassword(User user, string providedPassword);
    }
}
