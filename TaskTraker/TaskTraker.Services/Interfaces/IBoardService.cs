
using TaskApp.Data.Models;

namespace TaskApp.Services.Interfaces
{
    public interface IBoardService
    {
        Task<Board> CreateBoardAsync(Board board);
        Task<Board> GetBoardByIdAsync(int boardId);
        Task<IEnumerable<Board>> GetAllBoardsAsync();
    }
}
