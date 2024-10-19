using Microsoft.EntityFrameworkCore;
using TaskApp.Data.Context;
using TaskApp.Data.Models;
using TaskApp.Services.Interfaces;

namespace TaskTraker.Services.Services
{
    public class BoardService : IBoardService
    {
        private readonly TaskTrakerDbContext _context;

        public BoardService(TaskTrakerDbContext context)
        {
            _context = context;
        }

        public async Task<Board> CreateBoardAsync(Board board)
        {
        

            var existingBoard = await _context.Boards
                                              .FirstOrDefaultAsync(b => b.Name == board.Name);

            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
            return board;
        }

        public async Task<Board> GetBoardByIdAsync(int boardId)
        {
            Console.WriteLine($"Searching for board with ID: {boardId}");
            var board = await _context.Boards.FindAsync(boardId);
            return board;
        }

        public async Task DeleteBoardAsync(int boardId)
        {
            var board = await _context.Boards.FindAsync(boardId);

            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Board>> GetAllBoardsAsync()
        {
            return await _context.Boards.ToListAsync();
        }
    }
}
