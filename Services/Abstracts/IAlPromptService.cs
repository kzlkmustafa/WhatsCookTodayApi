﻿using WhatsCookTodayApi.MyModels;

namespace WhatsCookTodayApi.Services.Abstracts
{
    public interface IAlPromptService : IGenericService<AIPrompt>
    {
        Task<IQueryable<AIPrompt>> GetListAllForUser(int UserId);
    }
}
