using WhatsCookTodayApi.MyModels;
using WhatsCookTodayApi.Repository;
using WhatsCookTodayApi.Services.Abstracts;

namespace WhatsCookTodayApi.Services.Concrete
{
    public class SliderManager : ISliderService
    {
        IGenericRepository<Slider> _genericRepository;

        public SliderManager(IGenericRepository<Slider> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task Add(Slider entity)
        {
            await _genericRepository.Insert(entity);
        }

        public async Task Delete(int id)
        {
            Slider slider = await _genericRepository.GetById(id);
            await _genericRepository.Delete(slider);
        }

        public async Task<Slider> GetById(int id)
        {
            Slider slider = await _genericRepository.GetById(id);
            return slider;
        }

        public async Task<IQueryable<Slider>> GetListAllAsync()
        {
            var sliders = await _genericRepository.GetListAllAsync();
            return sliders;
        }

        public async Task Update(Slider entity)
        {
            await _genericRepository.Update(entity);
        }
    }
}
