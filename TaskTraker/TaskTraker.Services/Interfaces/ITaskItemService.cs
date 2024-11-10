using TaskTraker.Data.Models;
using TaskTraker.Services.Dtos;

namespace TaskTraker.Services.Interfaces
{
    public interface ITaskItemService
    {
        Task<IEnumerable<GetTaskItemDto>> GetAllByUserAsync();

        Task<IEnumerable<GetTaskItemDto>> GetAllByBoardAsync(int boardId);

        Task<GetTaskItemDto> GetAsync(int id, int? boardId = null);

        Task CreateAsync(CreateTaskItemDto itemDto);

        Task UpdateAsync(UpdateTaskItemDto itemDto);

        Task DeleteAsync(int id);
    }
}
