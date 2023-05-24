using WhatsCookTodayApi.MyModels;

namespace WhatsCookTodayApi.Services.Abstracts
{
    public interface IMyPromptService : IGenericService<MyPrompt>
    {
        Task<IQueryable<MyPrompt>> GetListAllForUser(string UserId);
    }
}
