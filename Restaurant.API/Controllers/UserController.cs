using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.APIComponents.Attributes;
using Restaurant.Data.Models.UserModels;
using Restaurant.DB.Enums;
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

        [HttpPost("AddUser")]
        public IActionResult AddUser([FromBody] UserCreateRequestDto userDto)
        {
            var id = _userService.AddUser(userDto);
            return Created($"api/User/{id}", null);
        }

        [AuthorizeWithRoles(RoleEnum.HeadAdmin)]
        [HttpPost("DisableUser/{id}")]
        public IActionResult DisableUser([FromRoute] long id)
        {
            var userClaims = User.Claims.ToList();
            _userService.DisableUser(id, userClaims);
            return Ok();
        }

        [HttpPost("SignInUser")]
        public IActionResult SignInUser([FromBody] LoginDto dto)
        {
            var token = _userService.SignInUser(dto);
            return Ok(token);
        }

        [HttpGet("GetUsersList")]
        public IActionResult GetUsersList()
        {
            var usersList = _userService.GetUsersList();
            return Ok(usersList);
        }
    }
}
