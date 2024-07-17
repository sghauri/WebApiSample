using DAL.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;


namespace WebApiSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemV2Controller : Controller
    {
        private readonly ITodoItemRepository _todoItemRepository;
        
        public TodoItemV2Controller(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        [EnableQuery(PageSize = 4)]
        [HttpGet(Name = "Get")]
        public IQueryable<TodoItem> Get()
        {
            return _todoItemRepository.GetAllAsQueryable();
        }
    }
}
