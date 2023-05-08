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

        [HttpPost]
        public async Task<IActionResult> PostPrompt(string searchstring)
        {
          //  await _myPromtService.Add(myprompt);
            string AIAnswer = await _AIService.GetAIAnswer(searchstring);
            return Ok(AIAnswer);
        }
    }
}
