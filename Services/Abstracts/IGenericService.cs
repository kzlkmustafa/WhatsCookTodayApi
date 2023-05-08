namespace WhatsCookTodayApi.Services.Abstracts
{
    public interface IGenericService<T>
    {
        Task Add(T entity);
        Task Delete(int id);
        Task<IQueryable<T>> GetListAllAsync();
        Task<T> GetById(int id);
        Task Update(T entity);

    }
}   
