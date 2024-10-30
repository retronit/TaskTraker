using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTraker.Data.Models;
using TaskTraker.Services.Dtos;
using TaskTraker.Services.Interfaces;

namespace TaskTraker.Api.Controllers
{
    [Authorize]
    [Route("api/board")]
    [ApiController]
    public class BoardController(IBoardService service) : ControllerBase
    {
        private readonly IBoardService _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Board>>> GetAllAsync()
        {
            var boards = await _service.GetAllAsync();
            return Ok(boards);
        }

        [HttpGet("{boardId}")]
        public async Task<ActionResult<Board>> GetAsync(int boardId)
        {
            var board = await _service.GetAsync(boardId);
            return Ok(board);
        }

        [HttpGet("{boardId}/collaborators")]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetAllCollaboratorsAsync(int boardId)
        {
            var collaborators = await _service.GetAllCollaboratorsAsync(boardId);
            return Ok(collaborators);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(CreateBoardDto boardDto)
        {
            await _service.CreateAsync(boardDto);
            return Ok();
        }

        [HttpPost("{boardId}/collaborators")]
        public async Task<IActionResult> AddCollaboratorsAsync(int boardId, [FromBody] List<string> userIds)
        {
            await _service.AddCollaboratorsAsync(boardId, userIds);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNameAsync(UpdateBoardDto boardDto)
        {
            await _service.UpdateNameAsync(boardDto);
            return Ok();
        }

        [HttpDelete("{boardId}")]
        public async Task<ActionResult> DeleteAsync(int boardId)
        {
            await _service.DeleteAsync(boardId);
            return Ok();
        }

        [HttpDelete("{boardId}/collaborators")]
        public async Task<ActionResult> DeleteAsync(int boardId, [FromBody] List<string> userIds)
        {
            await _service.RemoveCollaboratorsAsync(boardId, userIds);
            return Ok();
        }
    }
}

