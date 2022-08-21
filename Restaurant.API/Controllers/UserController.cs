﻿using Microsoft.AspNetCore.Mvc;
using Restaurant.APIComponents.Attributes;
using Restaurant.Data.Models.UserModels;
using Restaurant.DB.Enums;
using Restaurant.IServices;

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

        //[AuthorizeWithRoles(RoleEnum.HeadAdmin, RoleEnum.Admin)]
        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById([FromRoute] long id)
        {
            var user = _userService.GetUser(id);
            return Ok(user);
        }

        //[AuthorizeWithRoles(RoleEnum.HeadAdmin, RoleEnum.Admin)]
        [HttpGet("GetUsersList")]
        public IActionResult GetUsersList()
        {
            var usersList = _userService.GetUsers();
            return Ok(usersList);
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

        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser([FromRoute] long id, [FromBody] UserUpdateRequest userUpdateRequest)
        {
            _userService.UpdateUser(id, userUpdateRequest);
            return Ok();
        }

        [HttpPut("UpdateUserEmail/{id}")]
        public IActionResult UpdateUserEmail([FromRoute] long id, [FromBody] string newEmail)
        {
            _userService.UpdateUserEmail(id, newEmail);
            return Ok();
        }
    }
}
