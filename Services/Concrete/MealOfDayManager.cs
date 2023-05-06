using WhatsCookTodayApi.MyModels;
using WhatsCookTodayApi.Repository;
using WhatsCookTodayApi.Services.Abstracts;

namespace WhatsCookTodayApi.Services.Concrete
{
    public class MealOfDayManager : IMealOfDayService
    {
        IGenericRepository<MealOfDay> _repository;

        public MealOfDayManager(IGenericRepository<MealOfDay> repository)
        {
            _repository = repository;
        }

        public async Task Add(MealOfDay entity)
        {
            await _repository.Insert(entity);
        }

        public async Task Delete(int id)
        {
            MealOfDay mealOfDay = await _repository.GetById(id);
            await _repository.Delete(mealOfDay);
        }

        public async Task<MealOfDay> GetById(int id)
        {
            MealOfDay mealOfDay = await _repository.GetById(id);
            return mealOfDay;
        }

        public async Task<IQueryable<MealOfDay>> GetListAllAsync()
        {
            var mealOfDay = await _repository.GetListAllAsync();
            return mealOfDay;
        }

        public async Task Update(MealOfDay entity)
        {
            await _repository.Update(entity);
        }
    }
}
