using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        [HttpGet("GetIngredientList")]
        public IActionResult GetIngredientList()
        {

            return Ok();
        }

        [HttpGet("GetIngredientById/{id}")]
        public IActionResult GetIngredientById([FromRoute] int id)
        {

            return Ok();
        }

        [HttpPost("AddIngredient")]
        public IActionResult AddIngredient([FromBody] string ingredientName)
        {
            var id = 1;
            return Created($"api/Ingredient/GetIngredientById/{id}", null);
        }

        [HttpPut("UpdateIngredient/{id}")]
        public IActionResult UpdateIngredient([FromRoute] int id, [FromBody] string ingredientName)
        {

            return Ok();
        }

        [HttpDelete("DeleteIngredient/{id}")]
        public IActionResult DeleteIngredient([FromRoute] int id)
        {

            return NoContent();
        }
    }
}
