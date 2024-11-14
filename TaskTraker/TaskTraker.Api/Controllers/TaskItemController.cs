using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTraker.Data.Models;
using TaskTraker.Services.Dtos;
using TaskTraker.Services.Interfaces;

namespace TaskTraker.Api.Controllers
{
    [Authorize]
    [Route("api/task-items")]
    [ApiController]
    public class TaskItemsController(ITaskItemService service) : ControllerBase
    {
        private readonly ITaskItemService _service = service;

        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllByUserAsync()
        {
            var items = await _service.GetAllByUserAsync();
            return Ok(items);
        }

        [HttpGet("board/{boardId}")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllByBoardAsync(int boardId)
        {
            var items = await _service.GetAllByBoardAsync(boardId);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetAsync(int id, [FromQuery] int? boardId = null)
        {
            var taskItem = await _service.GetAsync(id, boardId);
            return Ok(taskItem);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateTaskItemDto taskItemDto)
        {
            await _service.CreateAsync(taskItemDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(UpdateTaskItemDto taskItemDto)
        {
            await _service.UpdateAsync(taskItemDto);
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
