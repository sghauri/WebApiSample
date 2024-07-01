using Microsoft.EntityFrameworkCore;
using WebApiSample.DataContext;
using Domain.Entities;

namespace WebApiSample.Repositories
{
    public class TodoItemRepository : EntityRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoDbContext dbContext) : base(dbContext) 
        { }

        public async Task<IEnumerable<TodoItem>> GetTasksByStatusAsync(string status)
        {
            return await TodoDbContext.TodoItems.Where(x => x.Status == status)
                        .OrderBy(x => x.CreatedDate)
                        .ToListAsync();
        }

        private TodoDbContext TodoDbContext
        {
            get
            {
                return _dbContext as TodoDbContext;
            }
        }
    }
}
