﻿using Microsoft.AspNetCore.Mvc;
using Restaurant.Authentication.Attributes;
using Restaurant.Business.IServices;
using Restaurant.Data.Models.MealModels;
using Restaurant.Entities.Enums;

namespace Restaurant.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [HttpGet("meals/{id}")]
        public IActionResult GetMeal([FromRoute] int id)
        {
            return Ok(_mealService.GetMeal(id));
        }

        [HttpGet("meals/groups")]
        public async Task<IActionResult> GetActiveMealsGroupedByCategory()
        {
            return Ok(await _mealService.GetActiveMealsGroupedByCategory());
        }

        [HttpGet("meals/page")]
        [AuthorizeWithRoles(RoleEnum.Admin)]
        public async Task<IActionResult> GetMealsPage([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 0)
        {
            return Ok(await _mealService.GetMealPage(pageIndex, pageSize));
        }

        [HttpPost("meals")]
        [AuthorizeWithRoles(RoleEnum.Admin)]
        public IActionResult AddMeal([FromBody] MealCreateRequest mealCreateRequest)
        {
            _mealService.AddMeal(mealCreateRequest);
            return Ok();
        }

        [HttpPut("meals/{id}")]
        [AuthorizeWithRoles(RoleEnum.Admin)]
        public IActionResult UpdateMeal([FromRoute] int id, [FromBody] MealUpdateRequest mealUpdateRequest)
        {
            _mealService.UpdateMeal(id, mealUpdateRequest);
            return Ok();
        }

        [HttpDelete("meals/{id}")]
        [AuthorizeWithRoles(RoleEnum.Admin)]
        public IActionResult DeleteMeal([FromRoute] int id)
        {
            _mealService.DeleteMeal(id);
            return NoContent();
        }

        [HttpPatch("meals/deactivate")]
        [AuthorizeWithRoles(RoleEnum.Admin)]
        public IActionResult SetMealAsUnavailable([FromBody] int id)
        {
            _mealService.SetMealAsUnavailable(id);
            return Ok();
        }

        [HttpPatch("meals/activate")]
        [AuthorizeWithRoles(RoleEnum.Admin)]
        public IActionResult SetMealAsAvailable([FromBody] int id)
        {
            _mealService.SetMealAsAvailable(id);
            return Ok();
        }

        [HttpPatch("meals/updatePrice/{id}/{newPrice}")]
        [AuthorizeWithRoles(RoleEnum.Admin)]
        public IActionResult UpdateMealsPrice([FromRoute] int id, [FromRoute] decimal newPrice)
        {
            _mealService.UpdateMealsPrice(id, newPrice);
            return Ok();
        }
    }
}
