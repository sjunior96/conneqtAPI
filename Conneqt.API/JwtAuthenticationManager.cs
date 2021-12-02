using Conneqt.API.Data.Repositories;
using Conneqt.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Conneqt.API
{
    public class JwtAuthenticationManager
    {
        private IUserRepository _userRepository;
        
        public JwtAuthenticationManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public JwtAuthResponse Authenticate(string email, string password)
        {
            var user = _userRepository.GetUser(email, password);

            if (user == null)
            {
                return null;
            }

            var tokenExpireTimeStamp = DateTime.UtcNow.AddMinutes(Constants.JWT_TOKEN_VALIDITY_MINS);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(Constants.JWT_SECURITY_KEY);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim("email", email),
                    new Claim(ClaimTypes.PrimaryGroupSid, "User Group 01")
                }),
                Expires = tokenExpireTimeStamp,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new JwtAuthResponse(user.CompanyName, user.Segment, user.NumberOfUsers, user.FullName, user.Email, user.Telephone, user.Password)
            {
                token = token,
                expiresIn = (int)tokenExpireTimeStamp.Subtract(DateTime.Now).TotalSeconds,
            };
        }
    }
}
