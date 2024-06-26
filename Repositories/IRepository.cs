using System.Linq.Expressions;
using WebApiSample.Models;

namespace WebApiSample.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        //void Update(T entity);
        void Delete(int id);
    }
}
