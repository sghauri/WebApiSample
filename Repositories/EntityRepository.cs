using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebApiSample.Repositories
{
    public class EntityRepository<T> : IRepository<T> where T : class
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
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
