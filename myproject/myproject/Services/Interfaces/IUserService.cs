using Microsoft.AspNetCore.Mvc;
using myproject.DAOs;
using myproject.Model;

namespace myproject.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> getAll();
        Task<string> login(LoginUserDTO loginUserDTO);
        Task<string> register(RegisterUserDTO registerUserDTO);
        bool VerifyPassword(User user, string providedPassword);
    }
}
