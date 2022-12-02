using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.MealCategoryModels;

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

        [HttpGet("GetMealCategory/{id}")]
        public IActionResult GetMealCategory([FromRoute] short id)
        {
            return Ok(_mealCategoryService.GetMealCategory(id));
        }

        [HttpGet("GetMealCategories")]
        public IActionResult GetMealCategories()
        {
            return Ok(_mealCategoryService.GetMealCategories());
        }

        [HttpGet("GetMealCategoriesPage")]
        public IActionResult GetMealCategoriesPage([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            return Ok(_mealCategoryService.GetMealCategoriesPage(pageIndex, pageSize));
        }

        [HttpPost()]
        public IActionResult Add([FromBody] MealCategoryCreateRequest mealCategoryCreateRequest)
        {
            var id = _mealCategoryService.AddMealCategory(mealCategoryCreateRequest.Name);
            return Ok();
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update([FromRoute] short id, [FromBody] MealCategoryUpdateRequest mealCategoryUpdateRequest)
        {
            _mealCategoryService.UpdateMealCategory(id, mealCategoryUpdateRequest.Name);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteMealCategory([FromRoute] short id)
        {
            _mealCategoryService.DeleteMealCategory(id);
            return NoContent();
        }
    }
}
