using Conneqt.API.Data.Repositories;
using Conneqt.API.Models;
using MongoDB.Driver;
using TaskManager.API.Data.Configurations;

namespace Conneqt.API.Repositories
{
    public class UserRepository :IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);

            _users = database.GetCollection<User>("users");
        }

        public User GetUser(string email, string password)
        {
            return _users.Find(user => user.Email == email && user.Password == password).FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return _users.Find(user => true).ToList();
        }

        public User UserRegister(User user)
        {
            _users.InsertOne(user);
            return user;
        }
    }
}
