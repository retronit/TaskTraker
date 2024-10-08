using TaskApp.Data.Models;

namespace TaskApp.Services.Interfaces
{
    public interface ITaskItemService
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();

        Task<TaskItem> GetAsync(int id);

        Task CreateAsync(TaskItem taskItem);

        Task UpdateAsync(int id, TaskItem taskItem);

        Task DeleteAsync(int id);
    }
}
