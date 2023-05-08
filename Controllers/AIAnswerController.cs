using Microsoft.AspNetCore.Mvc;
using WhatsCookTodayApi.Services.Abstracts;

namespace WhatsCookTodayApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AIAnswerController : ControllerBase
    {
        private readonly IAlPromptService _alPromptService;

        public AIAnswerController(IAlPromptService alPromptService)
        {
            _alPromptService = alPromptService;
        }

        [HttpPost("AddAIAnswerPrompt")]
        public async Task<IActionResult> AddAIAnswerPrompt(string promptmaterials)
        {
            return Ok();
        }


    }
}
