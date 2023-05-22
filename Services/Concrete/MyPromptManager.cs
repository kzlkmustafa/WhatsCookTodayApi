using WhatsCookTodayApi.MyModels;
using WhatsCookTodayApi.Repository;
using WhatsCookTodayApi.Services.Abstracts;

namespace WhatsCookTodayApi.Services.Concrete
{
    public class MyPromptManager : IMyPromptService
    {
        private readonly IGenericRepository<MyPrompt> _repository;

        public MyPromptManager(IGenericRepository<MyPrompt> repository)
        {
            _repository = repository;
        }

        public async Task Add(MyPrompt entity)
        {
            await _repository.Insert(entity);
        }

        public async Task Delete(int id)
        {
            MyPrompt myPrompt = await _repository.GetById(id);
            if (myPrompt != null)
            {
                await _repository.Delete(myPrompt);
            }
            else
            {
                throw new ArgumentException("Entity not found.");
            }
            return;
        }

        public async Task<MyPrompt> GetById(int id)
        {
            MyPrompt myPrompt = await _repository.GetById(id);
            return myPrompt;
        }

        public async Task<IQueryable<MyPrompt>> GetListAllAsync()
        {
            var myPrompt = await _repository.GetListAllAsync();
            return myPrompt;
        }

        public async Task<IQueryable<MyPrompt>> GetListAllForUser(int UserId)
        {
            
            var AllPrompt = await _repository.GetListAllAsync();
            var myPrompt = AllPrompt.Where(x => x.UserId == UserId);
            return myPrompt;
        }

        public async Task Update(MyPrompt entity)
        {
            await _repository.Update(entity);
        }
    }
}
