namespace Conneqt.API.Models
{
    [Serializable]
    public class JwtAuthResponse : User
    {
        public JwtAuthResponse(string companyName, string segment, int numberOfUsers, string fullName, string email, string telephone, string password) : base(companyName, segment, numberOfUsers, fullName, email, telephone, password)
        {
        }

        public string token { get; set; }
        public int expiresIn { get; set; }
    }
}
