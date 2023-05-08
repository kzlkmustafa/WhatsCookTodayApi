using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WhatsCookTodayApi.Data;

namespace WhatsCookTodayApi.Repository
{
    public class EfRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<T> _dbSet;

        public EfRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Set<T>();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IQueryable<T>> GetListAllAsync()
        {
            return await Task.FromResult(_dbSet.AsQueryable());
        }

        public async Task Insert(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbSet.Attach(entity);
            await _databaseContext.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AnyAsync(filter);
        }

    }
}
