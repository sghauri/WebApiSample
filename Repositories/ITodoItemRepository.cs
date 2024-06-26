using WebApiSample.Models;

namespace WebApiSample.Repositories
{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {
        Task<IEnumerable<TodoItem>> GetTasksByStatusAsync(string status);
    }
}
