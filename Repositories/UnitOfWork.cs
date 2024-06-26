using WebApiSample.DataContext;

namespace WebApiSample.Repositories
{
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
