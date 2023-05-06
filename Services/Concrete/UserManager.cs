using WhatsCookTodayApi.MyModels;
using WhatsCookTodayApi.Repository;
using WhatsCookTodayApi.Services.Abstracts;

namespace WhatsCookTodayApi.Services.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IGenericRepository<User> _repository;

        public UserManager(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task Add(User entity)
        {
            await _repository.Insert(entity);
        }

        public async Task Delete(int id)
        {
            User user = await _repository.GetById(id);
            await _repository.Delete(user);
        }

        public async Task<User> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IQueryable<User>> GetListAllAsync()
        {
            return await _repository.GetListAllAsync();
        }

        public async Task Update(User entity)
        {
            await _repository.Update(entity);
        }
    }
}
