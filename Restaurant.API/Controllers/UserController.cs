using Microsoft.AspNetCore.Mvc;
using Restaurant.Authentication.Attributes;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.UserModels.Requests;
using Restaurant.Entities.Enums;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult SignInUser([FromBody] LoginRequest loginRequest)
        {
            var user = _userService.SignInUser(loginRequest);
            return Ok(user);
        }
    }
}
