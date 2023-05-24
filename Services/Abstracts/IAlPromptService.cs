using WhatsCookTodayApi.MyModels;

namespace WhatsCookTodayApi.Services.Abstracts
{
    public interface IAlPromptService : IGenericService<AIPrompt>
    {
        Task<IQueryable<AIPrompt>> GetListAllForUser(string UserId);
        Task<IQueryable<AIPrompt>> GetAIAnswerForPrompt(int PromptId);
    }
}
