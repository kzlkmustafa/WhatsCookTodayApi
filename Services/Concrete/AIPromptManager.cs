using WhatsCookTodayApi.MyModels;
using WhatsCookTodayApi.Repository;
using WhatsCookTodayApi.Services.Abstracts;

namespace WhatsCookTodayApi.Services.Concrete
{
    public class AIPromptManager : IAlPromptService
    {
        private readonly IGenericRepository<AIPrompt> _genericRepository;

        public AIPromptManager(IGenericRepository<AIPrompt> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task Add(AIPrompt entity)
        {
            await _genericRepository.Insert(entity);
        }

        public async Task Delete(int id)
        {
            AIPrompt aIPrompt = await _genericRepository.GetById(id);
            await _genericRepository.Delete(aIPrompt);
        }

        public async Task<IQueryable<AIPrompt>> GetAIAnswerForPrompt(int PromptId)
        {
            var AnswerForPrompt = await _genericRepository.GetListAllAsync();
            var myAnswerForPrompt = AnswerForPrompt.Where(x => x.MyPromptId == PromptId);
            return myAnswerForPrompt;
        }

        public async Task<AIPrompt> GetById(int id)
        {
            AIPrompt aIPrompt = await _genericRepository.GetById(id);
            return aIPrompt;
        }

        public async Task<IQueryable<AIPrompt>> GetListAllAsync()
        {
            var aIPrompt = await _genericRepository.GetListAllAsync();
            return aIPrompt;
        }
        public async Task<IQueryable<AIPrompt>> GetListAllForUser(string UserId)
        {

            var AllAIPrompt = await _genericRepository.GetListAllAsync();
            var myAIPrompt = AllAIPrompt.Where(x => x.Id == UserId);
            return myAIPrompt;
        }

        public async Task Update(AIPrompt entity)
        {
            await _genericRepository.Update(entity);
        }
    }
}
