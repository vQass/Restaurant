using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        public CityController()
        {

        }

        [HttpGet("GetCityList")]
        public IActionResult GetCityList()
        {

            return Ok();
        }

        [HttpGet("GetCityById/{id}")]
        public IActionResult GetCityById([FromRoute] short id)
        {

            return Ok();
        }

        [HttpPost("AddCity")]
        public IActionResult AddCity([FromBody] string cityName)
        {
            short id = 0;
            return Created($"api/City/GetCityById/{id}", null);
        }

        [HttpPut("UpdateCity/{id}")]
        public IActionResult UpdateCity([FromRoute] short id, [FromBody] string cityName)
        {

            return Ok();
        }

        [HttpDelete("DeleteCity/{id}")]
        public IActionResult DeleteCity([FromRoute] short id)
        {

            return Ok();
        }

        [HttpPut("EnableCity/{id}")]
        public IActionResult EnableCity([FromRoute] short id)
        {

            return Ok();
        }

        [HttpPut("DisableCity/{id}")]
        public IActionResult DisableCity([FromRoute] short id)
        {

            return Ok();
        }
    }
}
