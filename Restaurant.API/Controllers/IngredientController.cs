using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.IngredientModels;

namespace Restaurant.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientServices;

        public IngredientController(IIngredientService ingredientServices)
        {
            _ingredientServices = ingredientServices;
        }

        [HttpGet("ingredients/{id}")]
        public IActionResult GetIngredient([FromRoute] int id)
        {
            return Ok(_ingredientServices.GetIngredient(id));
        }

        [HttpGet("ingredients")]
        public async Task<IActionResult> GetIngredients()
        {
            return Ok(await _ingredientServices.GetIngredients());
        }

        [HttpGet("ingredients/page")]
        public async Task<IActionResult> GetIngredientsForAdminPanel([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 0)
        {
            return Ok(await _ingredientServices.GetIngredientPage(pageIndex, pageSize));
        }

        [HttpPost("ingredients")]
        public IActionResult AddIngredient([FromBody] IngredientCreateRequest ingredientRequest)
        {
            _ingredientServices.AddIngredient(ingredientRequest);
            return Ok();
        }

        [HttpPut("ingredients/{id}")]
        public IActionResult UpdateIngredient([FromRoute] int id, [FromBody] IngredientUpdateRequest ingredientRequest)
        {
            _ingredientServices.UpdateIngredient(id, ingredientRequest);
            return Ok();
        }

        [HttpDelete("ingredients/{id}")]
        public IActionResult DeleteIngredient([FromRoute] int id)
        {
            _ingredientServices.DeleteIngredient(id);
            return NoContent();
        }
    }
}
