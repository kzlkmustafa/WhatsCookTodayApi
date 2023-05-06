using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;
using WhatsCookTodayApi.MyModels;

namespace WhatsCookTodayApi.Services.AIService
{
    public class OpenAIPromptService
    {
        readonly IOpenAIService _openAIService;

        public OpenAIPromptService(IOpenAIService openAIService)
        {
            _openAIService = openAIService;
        }
        public async Task<string> GetAIAnswer(string _prompt)
        {
            string getMyRecipe = "Submit 2 Turkish language recipes using the Ingredients here, each with up to 200 characters: " + _prompt;
            CompletionCreateResponse result = await _openAIService.Completions.CreateCompletion(new CompletionCreateRequest()
            {
                Prompt = getMyRecipe,
                MaxTokens = 500
            }, Models.TextDavinciV3);
            return result.Choices[0].Text;
        }

    }
}
