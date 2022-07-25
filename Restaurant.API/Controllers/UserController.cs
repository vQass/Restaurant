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
        public IActionResult AddUser([FromBody] UserCreateRequest userCreateRequest)
        {
            var id = _userService.AddUser(userCreateRequest);
            return Created($"api/User/{id}", null);
        }

        [AuthorizeWithRoles(RoleEnum.HeadAdmin, RoleEnum.Admin)]
        [HttpPost("DisableUser/{id}")]
        public IActionResult DisableUser([FromRoute] long id)
        {
            var userClaims = User.Claims.ToList();
            _userService.DisableUser(id, userClaims);
            return Ok();
        }

        [HttpPost("SignInUser")]
        public IActionResult SignInUser([FromBody] LoginRequest loginRequest)
        {
            var token = _userService.SignInUser(loginRequest);
            return Ok(token);
        }

        //[AuthorizeWithRoles(RoleEnum.HeadAdmin, RoleEnum.Admin)]
        [HttpGet("GetUsersList")]
        public IActionResult GetUsersList()
        {
            var usersList = _userService.GetUsersList();
            return Ok(usersList);
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(long id, [FromBody] UserUpdateRequest userUpdateRequest)
        {
            _userService.UpdateUser(id, userUpdateRequest);
            return Ok();
        }
    }
}
