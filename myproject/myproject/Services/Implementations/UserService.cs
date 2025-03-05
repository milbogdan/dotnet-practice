using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using myproject.Auth;
using myproject.DAOs;
using myproject.Model;
using myproject.Repositories.Interfaces;
using myproject.Services.Interfaces;

namespace myproject.Services.Implementations
{
    public class UserService: IUserService 
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly TokenProvider _tokenProvider;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,TokenProvider tokenProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenProvider = tokenProvider;
        }
        public async Task<IEnumerable<User>> getAll()
        {
            IEnumerable<User> lista = await _userRepository.getAll();
            return lista;
        }

        public async Task<string> login(LoginUserDTO loginUserDTO)
        {
            User user = await _userRepository.getByEmail(loginUserDTO.email);
            if (user == null)
            {
                return "user not found";
            }
            if (VerifyPassword(user, loginUserDTO.password))
            {
                return _tokenProvider.Create(user);
            }
            return "wrong password";
        }

        public async Task<string> register(RegisterUserDTO registerUserDTO)
        {
            User newUser = new User();
            newUser.Name = registerUserDTO.Name;
            newUser.Email = registerUserDTO.Email;
            newUser.CreatedAt = DateTime.UtcNow;
            await _userRepository.registerUser(newUser ,_passwordHasher.HashPassword(newUser,registerUserDTO.Password) );
            return "success";
        }

        public bool VerifyPassword(User user, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, providedPassword);
            return result == PasswordVerificationResult.Success;
        }

    }
}
