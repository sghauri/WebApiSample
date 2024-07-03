using DAL.DataContext;

namespace DAL.Repositories
{
    public interface IUnitOfWork
    {
        ITodoItemRepository TodoItems { get; }
        int Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoDbContext _dbContext;

        public ITodoItemRepository TodoItems { get; private set; }

        public UnitOfWork(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
            TodoItems = new TodoItemRepository(_dbContext);
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
