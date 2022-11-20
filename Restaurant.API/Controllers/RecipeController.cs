using Microsoft.AspNetCore.Mvc;
using Restaurant.IServices;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("GetRecipe/{mealId}")]
        public IActionResult GetRecipe([FromRoute] int mealId)
        {
            return Ok(_recipeService.GetRecipe(mealId));
        }

        [HttpGet("GetRecipeEditViewModel/{mealId}")]
        public IActionResult GetRecipeEditViewModel([FromRoute] int mealId)
        {
            return Ok(_recipeService.GetRecipeEditViewModel(mealId));
        }

        [HttpPut("UpdateMealRecipe/{mealId}")]
        public IActionResult UpdateMealRecipe([FromRoute] int mealId, [FromBody] List<int> ingredientsIds)
        {
            _recipeService.UpdateMealRecipe(mealId, ingredientsIds);
            return Ok();
        }
    }
}
