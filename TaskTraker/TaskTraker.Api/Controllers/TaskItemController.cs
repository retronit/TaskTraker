using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTraker.Data.Models;
using TaskTraker.Services.Interfaces;

namespace TaskTraker.Api.Controllers
{
    [Route("api/task-items")]
    [ApiController]
    public class TaskItemsController(ITaskItemService service) : ControllerBase
    {
        private readonly ITaskItemService _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllAsync()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetAsync(int id)
        {
            var taskItem = await _service.GetAsync(id);
            return Ok(taskItem);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(TaskItem taskItem)
        {
            await _service.CreateAsync(taskItem);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, TaskItem taskItem)
        {
            await _service.UpdateAsync(id, taskItem);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
