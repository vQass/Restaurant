using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Models.IngredientModels;
using Restaurant.IServices;

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
