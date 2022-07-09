using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Models.UserModels;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpPost]
        public void AddUser([FromBody] UserCreateRequestDto userDto)
        {

        }
    }
}
