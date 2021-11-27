using MongoDB.Bson;

namespace Conneqt.API.Models
{
    public class User
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string Segment { get; set; }
        public int NumberOfUsers { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Password { get; set; }

        public User(string companyName, string segment, int numberOfUsers, string fullName, string email, string telephone, string password)
        {
            Id = ObjectId.GenerateNewId().ToString();
            CompanyName = companyName;
            Segment = segment;
            NumberOfUsers = numberOfUsers;
            FullName = fullName;
            Email = email;
            Telephone = telephone;
            Password = password;
        }
    }
}
