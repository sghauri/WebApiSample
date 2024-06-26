using WebApiSample.Models;

namespace WebApiSample.Repositories
{
    public interface IUnitOfWork
    {
        ITodoItemRepository TodoItems { get; }
        int Commit();
    }
}
