using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using myproject.DAOs;
using myproject.Model;
using myproject.Repositories.Interfaces;
using myproject.Services.Interfaces;

namespace myproject.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<IEnumerable<User>> getAll()
        {
            IEnumerable<User> lista = await _userRepository.getAll();
            return lista;
        }

        public async Task<string> register(RegisterUserDAO registerUserDAO)
        {
            User newUser = new User();
            newUser.Name = registerUserDAO.name;
            newUser.Email = registerUserDAO.email;
            newUser.CreatedAt = DateTime.UtcNow;
            await _userRepository.registerUser(newUser ,_passwordHasher.HashPassword(newUser,registerUserDAO.password) );
            return "success";
        }

        public async Task<bool> VerifyPassword(User user, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, providedPassword);
            return result == PasswordVerificationResult.Success;
        }

    }
}
