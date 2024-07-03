using DAL.DataContext;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {
        Task<IEnumerable<TodoItem>> GetTasksByStatusAsync(string status);
    }

    public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
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
