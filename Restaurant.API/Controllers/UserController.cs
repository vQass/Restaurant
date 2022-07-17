using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Models.UserModels;
using Restaurant.IServices.Interfaces;

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

        [HttpPost]
        public IActionResult AddUser([FromBody] UserCreateRequestDto userDto)
        {
            var id = _userService.AddUser(userDto);
            return Created($"api/User/{id}", null);
        }
    }
}
