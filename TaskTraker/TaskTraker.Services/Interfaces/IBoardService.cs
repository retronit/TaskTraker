using TaskTraker.Data.Models;
using TaskTraker.Services.Dtos;

namespace TaskTraker.Services.Interfaces
{
    public interface IBoardService
    {
        Task<IEnumerable<GetBoardDto>> GetAllAsync();

        Task<GetBoardDto> GetAsync(int id);

        Task CreateAsync(CreateBoardDto board);

        Task UpdateNameAsync(UpdateBoardDto boardDto);

        Task DeleteAsync(int id);

        Task<IEnumerable<GetUserDto>> GetAllCollaboratorsAsync(int id);

        Task AddCollaboratorsAsync(int boardId, List<string> userIds);

        Task RemoveCollaboratorsAsync(int boardId, List<string> userIds);
    }
}
