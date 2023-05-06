using Microsoft.AspNetCore.Mvc;
using WhatsCookTodayApi.MyModels;
using WhatsCookTodayApi.Services.AIService;

namespace WhatsCookTodayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromptController : ControllerBase
    {
        OpenAIPromptService _AIService;

        public PromptController(OpenAIPromptService openAIPromptService)
        {
            _AIService = openAIPromptService;
        }

        [HttpPost]
        public async Task<IActionResult> PostPrompt(MyPrompt myprompt)
        {
            string AIAnswer = await _AIService.GetAIAnswer(myprompt.Materials);
            return Ok(AIAnswer);
        }
    }
}
