using Microsoft.AspNetCore.Mvc;
using WebApiSample.Middlewares;
using WebApiSample.Models;
using WebApiSample.Repositories;
using WebApiSample.Services;

namespace WebApiSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemController : Controller
    {
        private readonly IEnumerable<ICustomLogger> _customLoggers;
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TodoItemController(IEnumerable<ICustomLogger> customLoggers, ITodoItemRepository todoItemRepository, IUnitOfWork unitOfWork)
        {
            _customLoggers = customLoggers;
            _todoItemRepository = todoItemRepository;
            _unitOfWork = unitOfWork;
        }
        
        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> Get()
        {
            var items = await _todoItemRepository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var todoItem = await _todoItemRepository.GetAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }

        [HttpGet("getByStatus/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            var todoItems = await _todoItemRepository.GetTasksByStatusAsync(status);
            return Ok(todoItems);
        }

        [HttpPost(Name = nameof(Add))]
        [MiddlewareFilter(typeof(SecurityMiddlewareFilter))]
        public async Task<IActionResult> Add([FromBody][Bind("Title, Description, Status")] TodoItem todoItem)
        {
            todoItem.CreatedDate = DateTime.Now;

            var logMessage = $"TodoItemController -> Add() called with title: {todoItem.Title}, " +
                             $" description: {todoItem.Description}, status: {todoItem.Status}";
            LogMessage(logMessage);

            _unitOfWork.TodoItems.Add(todoItem);
            _unitOfWork.Commit();
                        
            if (todoItem.Id > 0)
            {
                var actionName = nameof(Get);
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                //return CreatedAtAction(actionName, new { Id = id }, null);
                //return CreatedAtAction(actionName, controllerName, new { Id = id }, null);

                var routeValues = new
                {
                    action = actionName,
                    Controller = controllerName,
                    Id = todoItem.Id
                };
                return CreatedAtRoute(routeValues, null);
            }
            return BadRequest();
        }

        [HttpPut("{id}", Name = nameof(Update))]
        [MiddlewareFilter(typeof(SecurityMiddlewareFilter))]
        public async Task<IActionResult> Update(int id, [FromBody][Bind("Title, Description, Status")] TodoItem todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest("TodoItem is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("TodoItem is invalid");
            }

            var existingTodoItem = await _todoItemRepository.GetAsync(id);
            if (existingTodoItem == null) 
            {
                return NotFound();
            }

            existingTodoItem.Title = todoItem.Title;
            existingTodoItem.Description = todoItem.Description;
            existingTodoItem.Status = todoItem.Status;

            _unitOfWork.TodoItems.Update(existingTodoItem);
            _unitOfWork.Commit();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [MiddlewareFilter(typeof(SecurityMiddlewareFilter))]
        public IActionResult Delete(int id)
        {
            try
            {
                _unitOfWork.TodoItems.Delete(id);
                _unitOfWork.Commit();
                return NoContent();
            }
            catch
            {
                return NotFound($"Todo item with id: {id} was not found.");
            }
        }

        private void LogMessage(string message) 
        {
            foreach (var logger in _customLoggers)
            {
                logger.Log(message);
            }
        }
    }
}
