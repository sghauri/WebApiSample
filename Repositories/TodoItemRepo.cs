using WebApiSample.Models;

namespace WebApiSample.Repositories
{
    public static class TodoItemRepo
    {
        private static List<TodoItem> _todoItems;
        private static int _sequentialId;

        static TodoItemRepo()
        {
            _sequentialId = 1;
            _todoItems = new List<TodoItem>();
            _todoItems.Add(new TodoItem
            {
                Id = _sequentialId,
                Title = "First item",
                CreatedDate = DateTime.Now.Date,
                LastUpdatedDate = null,
                Status = "Not Started"
            });

            _sequentialId++;
            _todoItems.Add(new TodoItem
            {
                Id = _sequentialId,
                Title = "Second item",
                CreatedDate = DateTime.Now,
                LastUpdatedDate = null,
                Status = "Not Started"
            });

            _sequentialId++;
            _todoItems.Add(new TodoItem
            {
                Id = _sequentialId,
                Title = "Third item",
                CreatedDate = DateTime.Now.Date,
                LastUpdatedDate = DateTime.Now,
                Status = "Started"
            });

            _sequentialId++;
            _todoItems.Add(new TodoItem
            {
                Id = _sequentialId,
                Title = "Fourth item",
                CreatedDate = DateTime.Now.Date,
                LastUpdatedDate = DateTime.Now,
                Status = "Completed"
            });
        }
        
        public static IEnumerable<TodoItem> GetTodoItems()
        {
            return _todoItems;
        }

        public static TodoItem? GetTodoItem(int id)
        {
            return _todoItems.FirstOrDefault(x => x.Id == id);
        }

        public static int SaveTodoItem(TodoItem item)
        {
            _sequentialId++;
            item.Id = _sequentialId;
            item.CreatedDate = DateTime.Now.Date;
            _todoItems.Add(item);
            return item.Id;
        }

        public static bool UpdateTodoItem(TodoItem item) 
        { 
            var todoItem = _todoItems.First(x => x.Id == item.Id);
            if (todoItem != null) 
            { 
                todoItem.Title = item.Title;
                todoItem.LastUpdatedDate = DateTime.Now;
                todoItem.Status = item.Status;
                todoItem.Description = item.Description;
                return true;
            }
            return false;
        }

        public static bool DeleteTodoItem(int id) 
        {
            var todoItem = _todoItems.First(x => x.Id == id);
            if (todoItem != null) 
            { 
                _todoItems.Remove(todoItem);
                return true;
            }
            return false;
        }
    }
}
