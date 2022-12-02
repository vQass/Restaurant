using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Models.CityModels;
using Restaurant.IServices;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("GetCities")]
        public IActionResult GetCities([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 0, [FromQuery] bool? cityActivity = null)
        {
            return Ok(_cityService.GetCities(cityActivity, pageIndex, pageSize));
        }

        [HttpGet("GetCity/{id}")]
        public IActionResult GetCity([FromRoute] short id)
        {
            return Ok(_cityService.GetCity(id));
        }

        [HttpPost("Add")]
        public IActionResult AddCity([FromBody] CityCreateRequest cityCreateRequest)
        {
            _cityService.AddCity(cityCreateRequest.Name);
            return Ok();
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateCity([FromRoute] short id, [FromBody] CityCreateRequest cityUpdateRequest)
        {
            _cityService.UpdateCity(id, cityUpdateRequest.Name);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteCity([FromRoute] short id)
        {
            _cityService.DeleteCity(id);
            return NoContent();
        }

        [HttpPut("Enable/{id}")]
        public IActionResult EnableCity([FromRoute] short id)
        {
            _cityService.EnableCity(id);
            return Ok();
        }

        [HttpPut("Disable/{id}")]
        public IActionResult DisableCity([FromRoute] short id)
        {
            _cityService.DisableCity(id);
            return Ok();
        }
    }
}
