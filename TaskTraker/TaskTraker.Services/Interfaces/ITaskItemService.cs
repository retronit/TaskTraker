using TaskTraker.Data.Models;
using TaskTraker.Services.Dtos;

namespace TaskTraker.Services.Interfaces
{
    public interface ITaskItemService
    {
        Task<IEnumerable<GetTaskItemDto>> GetAllAsync();

        Task<GetTaskItemDto> GetAsync(int id);

        Task CreateAsync(CreateTaskItemDto itemDto);

        Task UpdateAsync(UpdateTaskItemDto itemDto);

        Task DeleteAsync(int id);
    }
}
