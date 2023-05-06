using System.Linq.Expressions;

namespace WhatsCookTodayApi.Repository
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetById(int id);
        Task<IQueryable<T>> GetListAllAsync();
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);
    }
}
