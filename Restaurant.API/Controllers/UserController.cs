using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.UserModels.Requests;

namespace Restaurant.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("users/add")]
        public IActionResult AddUser([FromBody] UserCreateRequest userCreateRequest)
        {
            _userService.AddUser(userCreateRequest);
            return Ok();
        }

        [HttpPost("users/signIn")]
        public IActionResult SignIn([FromBody] LoginRequest loginRequest)
        {
            var user = _userService.SignInUser(loginRequest);
            return Ok(user);
        }
    }
}
