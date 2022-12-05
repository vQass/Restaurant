using Microsoft.AspNetCore.Mvc;
using Restaurant.Authentication.Attributes;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.MealCategoryModels;
using Restaurant.Entities.Enums;

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
        [AuthorizeWithRoles(RoleEnum.Admin)]
        public IActionResult GetMealCategoriesPage([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 0)
        {
            return Ok(_mealCategoryService.GetMealCategoriesPage(pageIndex, pageSize));
        }

        [HttpPost("mealCategories")]
        [AuthorizeWithRoles(RoleEnum.Admin)]
        public IActionResult AddMealCategory([FromBody] MealCategoryCreateRequest mealCategoryRequest)
        {
            _mealCategoryService.AddMealCategory(mealCategoryRequest);
            return Ok();
        }

        [HttpPut("mealCategories/{id}")]
        [AuthorizeWithRoles(RoleEnum.Admin)]
        public IActionResult UpdateMealCategory([FromRoute] short id, [FromBody] MealCategoryUpdateRequest mealCategoryRequest)
        {
            _mealCategoryService.UpdateMealCategory(id, mealCategoryRequest);
            return Ok();
        }

        [HttpDelete("mealCategories/{id}")]
        [AuthorizeWithRoles(RoleEnum.Admin)]
        public IActionResult DeleteMealCategory([FromRoute] short id)
        {
            _mealCategoryService.DeleteMealCategory(id);
            return NoContent();
        }
    }
}
