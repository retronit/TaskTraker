using TaskTraker.Data.Models;

namespace TaskTraker.Services.Interfaces
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
