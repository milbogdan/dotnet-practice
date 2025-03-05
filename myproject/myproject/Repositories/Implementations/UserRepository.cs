using Microsoft.EntityFrameworkCore;
using myproject.Model;
using myproject.Repositories.Interfaces;

namespace myproject.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> getAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> getByEmail(string email)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email==email);
            return user;

        }

        public async Task registerUser(User newUser, string password)
        {
            newUser.Password = password;
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }
    }
}
