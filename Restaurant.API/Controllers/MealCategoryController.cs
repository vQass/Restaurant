using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Models.MealCategoryModels;
using Restaurant.IServices;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealCategoryController : ControllerBase
    {
        private readonly IMealCategoryService _mealCategoryService;

        public MealCategoryController(IMealCategoryService mealCategoryService)
        {
            _mealCategoryService = mealCategoryService;
        }

        [HttpGet("GetMealCategories")]
        public IActionResult GetMealCategories()
        {
            return Ok(_mealCategoryService.GetMealCategories());
        }

        [HttpPost("AddMealCategory")]
        public IActionResult AddMealCategory([FromBody] MealCategoryCreateRequest mealCategoryCreateRequest)
        {
            var id = _mealCategoryService.AddMealCategory(mealCategoryCreateRequest.Name);
            return Created($"api/User/{id}", null);
        }

        [HttpPut("UpdateMealCategory/{id}")]
        public IActionResult UpdateMealCategory([FromRoute] short id, [FromBody] MealCategoryUpdateRequest mealCategoryUpdateRequest)
        {
            _mealCategoryService.UpdateMealCategory(id, mealCategoryUpdateRequest.Name);
            return Ok();
        }

        [HttpDelete("DeleteMealCategory/{id}")]
        public IActionResult DeleteMealCategory([FromRoute] short id)
        {
            _mealCategoryService.DeleteMealCategory(id);
            return NoContent();
        }
    }
}
