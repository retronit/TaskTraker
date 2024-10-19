using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskTraker.Data.Models;

using TaskTraker.Services.Interfaces;

namespace TaskTraker.Api.Controllers
{
    [ApiController]
    [Route("api/board")]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardsController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoard(Board board)
        {
           
            var createdBoard = await _boardService.CreateBoardAsync(board);
            return CreatedAtAction(nameof(GetBoardById), new { id = createdBoard.Id }, createdBoard);
            
     
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoardById(int id)
        {
           
           var board = await _boardService.GetBoardByIdAsync(id);
           return Ok(board);
        
       
        }



        [HttpGet]
        public async Task<IActionResult> GetAllBoards()
        {
            var boards = await _boardService.GetAllBoardsAsync();
            return Ok(boards);
        }
    }
}

