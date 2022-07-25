using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Models.MealCategoryModels;
using Restaurant.IServices.Interfaces;

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

        [HttpGet("GetAllMealsCategories")]
        public IActionResult GetAllMealsCategories()
        {
            var mealCategories = _mealCategoryService.GetAllMealsCategories();
            return Ok(mealCategories);
        }

        [HttpPost("AddMealCategory")]
        public IActionResult AddMealCategory([FromBody] MealCategoryCreateRequest mealCategoryCreateRequest)
        {
            var id = _mealCategoryService.AddMealCategory(mealCategoryCreateRequest);
            return Created($"api/User/{id}", null);
        }

        [HttpPut("UpdateMealCategory/{id}")]
        public IActionResult UpdateMealCategory([FromRoute] short id, [FromBody] MealCategoryUpdateRequest mealCategoryUpdateRequest)
        {
            _mealCategoryService.UpdateMealCategory(id, mealCategoryUpdateRequest);
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
