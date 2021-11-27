using Conneqt.API.Data.Repositories;
using Conneqt.API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Conneqt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet]
        [Route("getAllUsers")]
        public IActionResult Get()
        {
            var users = _userRepository.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("getUser")]

        public IActionResult Get(string email, string password)
        {
            var user = _userRepository.GetUser(email, password);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User newUser)
        {
            var user = new User(
                newUser.CompanyName, 
                newUser.Segment, 
                newUser.NumberOfUsers, 
                newUser.FullName, 
                newUser.Email, 
                newUser.Telephone, 
                newUser.Password
            );

            _userRepository.UserRegister(user);

            return Created("", user);
        }
    }
}
