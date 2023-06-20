using Microsoft.AspNetCore.Mvc;
using WhatsCookTodayApi.MyModels;
using WhatsCookTodayApi.Services.AIService;
using WhatsCookTodayApi.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using WhatsCookTodayApi.MyModels.ViewModels;

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
        public async Task<IActionResult> PostPrompt([FromBody] MyPromptViewModel model)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();
            try
            {
                if (!ModelState.IsValid)
                {
                    responseViewModel.IsSuccess = false;
                    responseViewModel.Message = "Bilgileriniz eksik, Lütfen tüm alanları doldurunuz.";
                    return BadRequest(responseViewModel);
                }
                string AIAnswer = await _AIService.GetAIAnswer(model.Materials);
                await AddPrompt(model.Materials, model.Id);
                await _answerController.AddAIAnswerPrompt(model.Materials, AIAnswer, model.Id);
                return Ok(AIAnswer);
            }
            catch (Exception ex)
            {
                responseViewModel.IsSuccess = false;
                responseViewModel.Message = ex.Message;

                return BadRequest(responseViewModel);
            }

            
        }
        //[HttpPost("AddPrompt")]
        private async Task<IActionResult> AddPrompt(string materials, string? UserId)
        {
            MyPrompt newModel = new MyPrompt();
            newModel.Materials = materials;
            newModel.Id = (UserId != "") ? UserId: null;
            await _myPromtService.Add(newModel);

            return Ok();
        }

        [HttpGet("GetPrompt")]
        public async Task<IActionResult> GetPrompt(int id)
        {
            var getprompt = await _myPromtService.GetById(id);
            return Ok(getprompt);
        }
        [Authorize]
        [HttpGet("GetAllPrompt")]
        public async Task<IActionResult> GetListAll()
        {
            var getAll = await _myPromtService.GetListAllAsync();
            return Ok(getAll);
        }
        [HttpGet("GetAllPromptForUser")]
        public async Task<IActionResult> GetListAllWithUser(string UserId)
        {
            var getAllforuser = await _myPromtService.GetListAllForUser(UserId);
            return Ok(getAllforuser);
        }
        [Authorize]
        [HttpDelete("DeletePrompt")]
        public async Task<IActionResult> DeletePrompt(int id)
        {
            await _myPromtService.Delete(id);
            return Ok();
        }
        [Authorize]
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
