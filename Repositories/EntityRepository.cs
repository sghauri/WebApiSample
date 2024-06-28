﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiSample.Models;

namespace WebApiSample.Repositories
{
    public class EntityRepository<T> : IRepository<T> where T : BaseEntity 
    {
        protected readonly DbContext _dbContext;
        public EntityRepository(DbContext dbContext) 
        { 
            _dbContext = dbContext;
        }
        
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Delete(int id)
        {
            var entity = _dbContext.Set<T>().Find(id);
            if (entity != null)
            {
                _dbContext.Remove<T>(entity);
            }
            else
                throw new Exception("Not found");
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            var entity = await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}