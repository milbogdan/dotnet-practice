using myproject.Model;

namespace myproject.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> getAll();
        Task registerUser(User user, string password);
        Task<User> getByEmail(string email);
    }
}
