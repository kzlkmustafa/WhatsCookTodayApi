using WhatsCookTodayApi.MyModels;
using WhatsCookTodayApi.Repository;
using WhatsCookTodayApi.Services.Abstracts;

namespace WhatsCookTodayApi.Services.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IGenericRepository<MyUsers> _repository;


        public UserManager(IGenericRepository<MyUsers> repository)
        {
            _repository = repository;
        }

        public async Task Add(MyUsers entity)
        {
            await _repository.Insert(entity);
        }

        public async Task Delete(int id)
        {
            MyUsers user = await _repository.GetById(id);
            await _repository.Delete(user);
        }

        public async Task<MyUsers> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IQueryable<MyUsers>> GetListAllAsync()
        {
            return await _repository.GetListAllAsync();
        }

        public async Task Update(MyUsers entity)
        {
            await _repository.Update(entity);
        }

    }
}
