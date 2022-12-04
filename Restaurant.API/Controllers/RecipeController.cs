using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.IServices;

namespace Restaurant.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("recipies/{mealId}")]
        public IActionResult GetRecipeEditViewModel([FromRoute] int mealId)
        {
            return Ok(_recipeService.GetRecipeEditViewModel(mealId));
        }

        [HttpPut("recipies/{mealId}")]
        public IActionResult UpdateMealRecipe([FromRoute] int mealId, [FromBody] List<int> ingredientsIds)
        {
            _recipeService.UpdateMealRecipe(mealId, ingredientsIds);
            return Ok();
        }
    }
}
