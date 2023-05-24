using Microsoft.AspNetCore.Mvc;
using WhatsCookTodayApi.MyModels;
using WhatsCookTodayApi.Services.Abstracts;

namespace WhatsCookTodayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AIAnswerController : ControllerBase
    {
        private readonly IAlPromptService _alPromptService;
        private readonly IMyPromptService _myPromptService;

        public AIAnswerController(IAlPromptService alPromptService, IMyPromptService myPromptService)
        {
            _alPromptService = alPromptService;
            _myPromptService = myPromptService;
        }

        [HttpPost("AddAIAnswer")]
        public async Task<IActionResult> AddAIAnswerPrompt(string _promptmaterials, string _AIAnswer, string _UserId)
        {
            AIPrompt aIPrompt = new AIPrompt();
            var AllPrompt = await _myPromptService.GetListAllAsync();
            foreach (var item in AllPrompt)
            {
                if (item.Materials == _promptmaterials)
                {
                    aIPrompt.MyPromptId = item.MyPromptId;
                }
            }

            aIPrompt.MyPromptsMaterials = _promptmaterials;
            aIPrompt.AIPromptRecipe = _AIAnswer;
            aIPrompt.Id = _UserId;

            await _alPromptService.Add(aIPrompt);
            return Ok();
        }

        [HttpGet("GetAIAnswer")]
        public async Task<IActionResult> GetAIAnswerPrompt(int id)
        {
            var aIPrompt = await _alPromptService.GetById(id);
            return Ok(aIPrompt);
        }
        [HttpGet("GetAllAIAnswer")]
        public async Task<IActionResult> GetAllAIAnswerPrompt()
        {
            var aIprompt = await _alPromptService.GetListAllAsync();
            return Ok(aIprompt);
        }
        [HttpGet("GetAllAIAnswerForUser")]
        public async Task<IActionResult> GetListAllWithUser(string UserId)
        {
            var getAllforuser = await _alPromptService.GetListAllForUser(UserId);
            return Ok(getAllforuser);
        }
        [HttpGet("GetAIAnswerForPrompt")]
        public async Task<IActionResult> GetAIAnswerForPrompt(int PromptId)
        {
            var getAllforprompt = await _alPromptService.GetAIAnswerForPrompt(PromptId);
            return Ok(getAllforprompt);
        }

        [HttpDelete("DeleteAIAnswer")]
        public async Task<IActionResult> DeleteAIAnswerPrompt(int id)
        {
            await _alPromptService.Delete(id);
            return Ok();
        }
        [HttpPut("UpdateAIAnswer")]
        public async Task<IActionResult> UpdateAIAnswerPrompt(int AIAnswerId, string promptMaterials, string AIAnswerRecipe)
        {
            var updateAIanswer = await _alPromptService.GetById(AIAnswerId);
            updateAIanswer.MyPromptsMaterials = promptMaterials;
            updateAIanswer.AIPromptRecipe = AIAnswerRecipe;
            await _alPromptService.Update(updateAIanswer);

            return Ok();

        }
    }
}
