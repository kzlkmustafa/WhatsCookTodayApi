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
        private readonly OpenAIPromptService _AIService;
        private readonly IMyPromptService _myPromtService;
        private readonly AIAnswerController _answerController;

        public PromptController(OpenAIPromptService aIService, IMyPromptService myPromtService, AIAnswerController answerController)
        {
            _AIService = aIService;
            _myPromtService = myPromtService;
            _answerController = answerController;
        }

        [HttpPost("PostAI")]
        public async Task<IActionResult> PostPrompt(string materials, int Userid)
        {
            string AIAnswer = await _AIService.GetAIAnswer(materials);
            await AddPrompt(materials, Userid);
            await _answerController.AddAIAnswerPrompt(materials, AIAnswer, Userid);
            return Ok(AIAnswer);
        }
        //[HttpPost("AddPrompt")]
        private async Task<IActionResult> AddPrompt(string materials, int UserId)
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

        [HttpGet("GetAllPrompt")]
        public async Task<IActionResult> GetListAll()
        {
            var getAll = await _myPromtService.GetListAllAsync();
            return Ok(getAll);
        }
        [HttpGet("GetAllPromptForUser")]
        public async Task<IActionResult> GetListAllWithUser(int UserId)
        {
            var getAllforuser = await _myPromtService.GetListAllForUser(UserId);
            return Ok(getAllforuser);
        }
        [HttpDelete("DeletePrompt")]
        public async Task<IActionResult> DeletePrompt(int id)
        {
            await _myPromtService.Delete(id);
            return Ok();
        }
        [HttpPut("UpdatePrompt")]
        public async Task<IActionResult> UpdatePrompt(MyPrompt myPrompt)
        {
            var updateprompt = await _myPromtService.GetById(myPrompt.MyPromptId);
            updateprompt.Materials = myPrompt.Materials;

            await _myPromtService.Update(updateprompt);
            return Ok();
        }
    }
}
