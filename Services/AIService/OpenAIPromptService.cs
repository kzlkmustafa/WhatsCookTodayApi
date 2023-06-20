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

            string getMyRecipe =
                " Sen verdiğim ürünlere göre bana 3 farklı yemek tarifi veren bir aşcısın" +
                " Ek bilgi sormayın, kişisel bilgi istemeyin." +
                " eğer ürünlerden biri ile yemek malzemesi değilse \"[0] [ERROR] [Bu malzeme ile yemek yapılamaz.]\" yazın ve bitirin. yapılabiliyorsa devam edin." +
                " Eğer yemek yapılabiliyorsa 3 tarifi de [yemeğin ismi][yemekte kullanılacak ürünler][yemeğin uzun anlatımlı tarifi] şeklinde köşeli parantez kullanarak anlatın." +
                " Her tarif için maksimum 300 karakter kullanın.";
                
            var result = await _openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromUser(getMyRecipe),
                    ChatMessage.FromSystem("Tamamdır"),
                    ChatMessage.FromUser("Ürünlerim: " + _prompt)
                },
                MaxTokens = 950,
                Model = Models.ChatGpt3_5Turbo
            });
            if (result.Successful)
            {
                return result.Choices.First().Message.Content;
            }
            else
            {
                if (result.Error == null)
                {
                    throw new Exception("Unknown Error");
                }

                return $"{result.Error.Code}: {result.Error.Message}";
            }
        }
        
    }
}
