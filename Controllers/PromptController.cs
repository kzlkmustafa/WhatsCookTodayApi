using Microsoft.AspNetCore.Mvc;
using WhatsCookTodayApi.MyModels;
using WhatsCookTodayApi.Services.AIService;
using WhatsCookTodayApi.Services.Abstracts;


namespace WhatsCookTodayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromptController : ControllerBase
    {
        OpenAIPromptService _AIService;
        IMyPromptService _myPromtService;

        public PromptController(OpenAIPromptService aIService, IMyPromptService myPromtService)
        {
            _AIService = aIService;
            _myPromtService = myPromtService;
        }

        [HttpPost("PostAI")]
        public async Task<IActionResult> PostPrompt(string materials)
        {
            string AIAnswer = await _AIService.GetAIAnswer(materials);
            
            return Ok(AIAnswer);
        }
        [HttpPost("AddPrompt")]
        public async Task<IActionResult> AddPrompt(string materials, int UserId)
        {
            MyPrompt newModel = new MyPrompt();
            newModel.Materials = materials;
            newModel.UserId = UserId;
            await _myPromtService.Add(newModel);
            return Ok();
        }
        [HttpGet("GetPrompt")]
        public async Task<IActionResult> GetPrompt(int id)
        {
            var getprompt = await _myPromtService.GetById(id);
            return Ok(getprompt);
        }
        [HttpGet("GetAllPromptForUser")]
        public async Task<IActionResult> GetListAllWithUser(int UserId)
        {
            var getAll = await _myPromtService.GetListAllAsync();
            return Ok(getAll);
        }
        [HttpDelete("DeletePrompt")]
        public async Task<IActionResult> DeletePrompt(int id)
        {
            await _myPromtService.Delete(id);
            return Ok();
        }
        [HttpPut("UpdatePrompt")]
        public async Task<IActionResult> UpdatePrompt(int id)
        {
            var updateprompt = await _myPromtService.GetById(id);
            await _myPromtService.Update(updateprompt);
            return Ok();
        }
    }
}
