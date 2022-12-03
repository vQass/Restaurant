using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.CityModels;

namespace Restaurant.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("cities/{id}")]
        public IActionResult GetCity([FromRoute] short id)
        {
            return Ok(_cityService.GetCity(id));
        }

        [HttpGet("cities")]
        public IActionResult GetCities([FromQuery] bool? cityActivity = null)
        {
            return Ok(_cityService.GetCities(cityActivity));
        }

        [HttpGet("cities/page")]
        public IActionResult GetCityPage([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 0)
        {
            return Ok(_cityService.GetCityPage(pageIndex, pageSize));
        }

        [HttpPost("cities")]
        public IActionResult AddCity([FromBody] CityCreateRequest cityCreateRequest)
        {
            _cityService.AddCity(cityCreateRequest);
            return Ok();
        }

        [HttpPut("cities/{id}")]
        public IActionResult UpdateCity([FromRoute] short id, [FromBody] CityUpdateRequest cityUpdateRequest)
        {
            _cityService.UpdateCity(id, cityUpdateRequest);
            return Ok();
        }

        [HttpDelete("cities/{id}")]
        public IActionResult DeleteCity([FromRoute] short id)
        {
            _cityService.DeleteCity(id);
            return NoContent();
        }

        [HttpPut("cities/enable/{id}")]
        public IActionResult EnableCity([FromRoute] short id)
        {
            _cityService.EnableCity(id);
            return Ok();
        }

        [HttpPut("cities/disable/{id}")]
        public IActionResult DisableCity([FromRoute] short id)
        {
            _cityService.DisableCity(id);
            return Ok();
        }
    }
}
