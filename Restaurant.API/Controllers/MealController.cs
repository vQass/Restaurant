using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Models.MealModels;
using Restaurant.IServices;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet("GetMeals")]
        public async Task<IActionResult> GetMeals()
        {
            return Ok(await _mealService.GetMeals());
        }

        [HttpGet("GetMealForAdminPanel/{id}")]
        public IActionResult GetMealForAdminPanel([FromRoute] int id)
        {
            return Ok(_mealService.GetMealForAdminPanel(id));
        }

        [HttpGet("GetMealsForAdminPanel")]
        public async Task<IActionResult> GetMealsForAdminPanel()
        {
            return Ok(await _mealService.GetMealsForAdminPanel());
        }

        [HttpGet("GetActiveMealsGroupedByCategory")]
        public async Task<IActionResult> GetActiveMealsGroupedByCategory()
        {
            return Ok(await _mealService.GetActiveMealsGroupedByCategory());
        }

        [HttpPost("AddMeal")]
        public IActionResult AddMeal([FromBody] MealCreateRequest mealCreateRequest)
        {
            var id = _mealService.AddMeal(mealCreateRequest);
            return Created($"api/User/{id}", null);
        }

        [HttpPut("UpdateMeal/{id}")]
        public IActionResult UpdateMeal([FromRoute] int id, [FromBody] MealUpdateRequest mealUpdateRequest)
        {
            _mealService.UpdateMeal(id, mealUpdateRequest);
            return Ok();
        }

        [HttpDelete("DeleteMeal/{id}")]
        public IActionResult DeleteMeal([FromRoute] int id)
        {
            _mealService.DeleteMeal(id);
            return NoContent();
        }

        [HttpPatch("SetMealAsUnavailable")]
        public IActionResult SetMealAsUnavailable([FromBody] int id)
        {
            _mealService.SetMealAsUnavailable(id);
            return Ok();
        }

        [HttpPatch("SetMealAsAvailable")]
        public IActionResult SetMealAsAvailable([FromBody] int id)
        {
            _mealService.SetMealAsAvailable(id);
            return Ok();
        }

        [HttpPatch("UpdateMealsPrice/{id}/{newPrice}")]
        public IActionResult UpdateMealsPrice([FromRoute] int id, [FromRoute] decimal newPrice)
        {
            _mealService.UpdateMealsPrice(id, newPrice);
            return Ok();
        }
    }
}
