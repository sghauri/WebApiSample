using Microsoft.AspNetCore.Mvc;
using WebApiSample.Middlewares;
using WebApiSample.Models;
using WebApiSample.Repositories;

namespace WebApiSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemController : Controller
    {
        [HttpGet(Name ="GetAll")]
        public IEnumerable<TodoItem> Get()
        {
            return TodoItemRepository.GetTodoItems();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var todoItem = TodoItemRepository.GetTodoItem(id);
            if (todoItem == null) 
            { 
                return NotFound();
            }
            return Ok(todoItem);
        }

        [HttpPost(Name = nameof(Add))]
        [MiddlewareFilter(typeof(SecurityMiddlewareFilter))]
        public IActionResult Add(string title, string description, string status)
        {
            var todoItem = new TodoItem
            {
                Title = title,
                Description = description,
                Status = status
            };

            var id = TodoItemRepository.SaveTodoItem(todoItem);
            if (id > 0)
            {
                var actionName = nameof(Get);
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                //return CreatedAtAction(actionName, new { Id = id }, null);
                //return CreatedAtAction(actionName, controllerName, new { Id = id }, null);

                var routeValues = new
                {
                    action = actionName,
                    Controller = controllerName,
                    Id = id
                };
                return CreatedAtRoute(routeValues, null);
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id) 
        { 
            if (TodoItemRepository.DeleteTodoItem(id))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
