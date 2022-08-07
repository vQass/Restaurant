using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("GetIngredientList")]
        public async Task<IActionResult> GetIngredientList()
        {
            return Ok(await _ingredientServices.GetIngredientsList());
        }

        [HttpGet("GetIngredientById/{id}")]
        public IActionResult GetIngredientById([FromRoute] int id)
        {
            return Ok(_ingredientServices.GetIngredientById(id));
        }

        [HttpPost("AddIngredient")]
        public IActionResult AddIngredient([FromBody] string ingredientName)
        {
            var id = _ingredientServices.AddIngredient(ingredientName);
            return Created($"api/Ingredient/GetIngredientById/{id}", null);
        }

        [HttpPut("UpdateIngredient/{id}")]
        public IActionResult UpdateIngredient([FromRoute] int id, [FromBody] string ingredientName)
        {
            _ingredientServices.UpdateIngredient(id, ingredientName);
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
