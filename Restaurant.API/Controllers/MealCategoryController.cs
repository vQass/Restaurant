using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.MealCategoryModels;

namespace Restaurant.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class MealCategoryController : ControllerBase
    {
        private readonly IMealCategoryService _mealCategoryService;

        public MealCategoryController(IMealCategoryService mealCategoryService)
        {
            _mealCategoryService = mealCategoryService;
        }

        [HttpGet("mealCategories/{id}")]
        public IActionResult GetMealCategory([FromRoute] short id)
        {
            return Ok(_mealCategoryService.GetMealCategory(id));
        }

        [HttpGet("mealCategories")]
        public IActionResult GetMealCategories()
        {
            return Ok(_mealCategoryService.GetMealCategories());
        }

        [HttpGet("mealCategories/page")]
        public IActionResult GetMealCategoriesPage([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 0)
        {
            return Ok(_mealCategoryService.GetMealCategoriesPage(pageIndex, pageSize));
        }

        [HttpPost("mealCategories")]
        public IActionResult Add([FromBody] MealCategoryCreateRequest mealCategoryRequest)
        {
            _mealCategoryService.AddMealCategory(mealCategoryRequest);
            return Ok();
        }

        [HttpPut("mealCategories/{id}")]
        public IActionResult Update([FromRoute] short id, [FromBody] MealCategoryUpdateRequest mealCategoryRequest)
        {
            _mealCategoryService.UpdateMealCategory(id, mealCategoryRequest);
            return Ok();
        }

        [HttpDelete("mealCategories/{id}")]
        public IActionResult DeleteMealCategory([FromRoute] short id)
        {
            _mealCategoryService.DeleteMealCategory(id);
            return NoContent();
        }
    }
}
