using Microsoft.AspNetCore.Mvc;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.IngredientModels;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientServices;

        public IngredientController(IIngredientService ingredientServices)
        {
            _ingredientServices = ingredientServices;
        }

        [HttpGet("GetIngredients")]
        public async Task<IActionResult> GetIngredients()
        {
            return Ok(await _ingredientServices.GetIngredients());
        }

        [HttpGet("GetIngredientsForAdminPanel")]
        public async Task<IActionResult> GetIngredientsForAdminPanel([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 0)
        {
            return Ok(await _ingredientServices.GetIngredientsForAdminPanel(pageIndex, pageSize));
        }

        [HttpGet("GetIngredient/{id}")]
        public IActionResult GetIngredient([FromRoute] int id)
        {
            return Ok(_ingredientServices.GetIngredient(id));
        }

        [HttpPost("AddIngredient")]
        public IActionResult AddIngredient([FromBody] IngredientCreateRequest ingredientCreateRequest)
        {
            var id = _ingredientServices.AddIngredient(ingredientCreateRequest.Name);
            return Created($"api/Ingredient/GetIngredientById/{id}", null);
        }

        [HttpPut("UpdateIngredient/{id}")]
        public IActionResult UpdateIngredient([FromRoute] int id, [FromBody] IngredientUpdateRequest ingredientUpdateRequest)
        {
            _ingredientServices.UpdateIngredient(id, ingredientUpdateRequest.Name);
            return Ok();
        }

        [HttpDelete("DeleteIngredient/{id}")]
        public IActionResult DeleteIngredient([FromRoute] int id)
        {
            _ingredientServices.DeleteIngredient(id);
            return NoContent();
        }
    }
}
