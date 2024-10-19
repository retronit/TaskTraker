using TaskTraker.Data.Models;

namespace TaskTraker.Services.Interfaces
{
    public interface IBoardService
    {
        Task<Board> CreateBoardAsync(Board board);
        Task<Board> GetBoardByIdAsync(int boardId);
        Task<IEnumerable<Board>> GetAllBoardsAsync();
    }
}
