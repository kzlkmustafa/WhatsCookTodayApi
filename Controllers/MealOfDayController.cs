using Microsoft.AspNetCore.Mvc;
using WhatsCookTodayApi.MyModels;
using WhatsCookTodayApi.Services.Abstracts;
using WhatsCookTodayApi.Services.AIService;

namespace WhatsCookTodayApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MealOfDayController : ControllerBase
    {
        private readonly IMealOfDayService _mealOfDayService;

        public MealOfDayController(IMealOfDayService mealOfDayService)
        {
            _mealOfDayService = mealOfDayService;
        }

        [HttpGet("GetMealofDay")]
        public async Task<IActionResult> GetMealOfDay(int id) {
            var getMeal = await _mealOfDayService.GetById(id);
            return Ok(getMeal);
            
        }
        [HttpGet("GetAllMealofDay")]
        public async Task<IActionResult> GetAllMealOfDay()
        {
            var getAllMeal = await _mealOfDayService.GetListAllAsync();
            return Ok(getAllMeal);
        }
        [HttpPost("AddMealofDay")]
        public async Task<IActionResult> AddMealOfDay(string MealName, string MealRecipe, string MealPhoto = " ")
        {
            MealOfDay mealOfDay = new MealOfDay();
            mealOfDay.MealOfDayName = MealName;
            mealOfDay.MealOfDayRecipe = MealRecipe;
            mealOfDay.MealOfDayPhoto = MealPhoto;
            await _mealOfDayService.Add(mealOfDay);
            return Ok();
        }

        [HttpDelete("DeleteMealofDay")]
        public async Task<IActionResult> DeleteMealOfDay(int id)
        {
            await _mealOfDayService.Delete(id);
            return Ok();
        }
        [HttpPut("UpdateMealofDay")]
        public async Task<IActionResult> UpdateMealOfDay(MealOfDay mealOfDay)
        {
            var updateMeal = await _mealOfDayService.GetById(mealOfDay.MealOfDayId);
            updateMeal.MealOfDayName = mealOfDay.MealOfDayName;
            updateMeal.MealOfDayRecipe = mealOfDay.MealOfDayRecipe;
            updateMeal.MealOfDayPhoto = mealOfDay.MealOfDayPhoto;

            return Ok();
        }
    }
}
