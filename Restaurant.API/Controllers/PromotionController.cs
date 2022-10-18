using Microsoft.AspNetCore.Mvc;
using Restaurant.Data.Models.PromotionModels;
using Restaurant.IServices;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet("GetPromotionsList")]
        public IActionResult GetPromotionsList()
        {
            return Ok(_promotionService.GetPromotionsList());
        }

        [HttpGet("GetPromotionById/{id}")]
        public IActionResult GetPromotionById([FromRoute] long id)
        {
            return Ok(_promotionService.GetPromotionById(id));
        }

        [HttpPost("AddPromotion")]
        public IActionResult AddPromotion([FromBody] PromotionCreateRequest promotion)
        {
            var id = _promotionService.AddPromotion(promotion);
            return Created($"api/City/GetCityById/{id}", null);
        }

        [HttpPut("UpdatePromotion/{id}")]
        public IActionResult UpdatePromotion([FromRoute] long id, [FromBody] PromotionUpdateRequest promotion)
        {
            _promotionService.UpdatePromotion(id, promotion);
            return Ok();
        }

        [HttpDelete("DeletePromotion/{id}")]
        public IActionResult DeletePromotion([FromRoute] long id)
        {
            _promotionService.DeletePromotion(id);
            return Ok();
        }


        [HttpPut("DisablePromotion/{id}")]
        public IActionResult DisablePromotion([FromRoute] long id)
        {
            _promotionService.DisablePromotion(id);
            return Ok();
        }
    }
}
