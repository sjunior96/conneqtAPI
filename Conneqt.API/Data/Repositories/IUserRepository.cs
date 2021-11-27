using Conneqt.API.Models;

namespace Conneqt.API.Data.Repositories
{
    public interface IUserRepository
    {
        User UserRegister(User user);

        IEnumerable<User> GetUsers();

        User GetUser(string email, string password);
    }
}
