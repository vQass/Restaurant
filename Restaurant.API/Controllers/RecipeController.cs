using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Models.RecipeModels;
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

        [HttpGet("GetRecipes")]
        public async Task<IActionResult> GetRecipes()
        {
            return Ok(await _recipeService.GetRecipes());
        }

        [HttpGet("GetRecipe/{mealId}")]
        public IActionResult GetRecipe([FromRoute] int mealId)
        {
            return Ok(_recipeService.GetRecipe(mealId));
        }

        [HttpGet("GetRecipeElement/{mealId}/{ingredientId}")]
        public IActionResult GetRecipeElement([FromRoute] int mealId, [FromRoute] int ingredientId)
        {
            return Ok(_recipeService.GetRecipeElementViewModel(mealId, ingredientId));
        }

        [HttpPost("AddRecipeElement")]
        public IActionResult AddRecipeElement([FromBody] RecipeCreateRequest recipeCreateRequest)
        {
            var ids = _recipeService.AddRecipeElement(recipeCreateRequest);

            return Created($"GetRecipeElement/{ids}", null);
        }

        [HttpDelete("DeleteRecipeElement/{mealId}/{ingredientId}")]
        public IActionResult DeleteRecipeElement([FromRoute] int mealId, [FromRoute] int ingredientId)
        {
            _recipeService.DeleteRecipeElement(mealId, ingredientId);
            return NoContent();
        }
    }
}
