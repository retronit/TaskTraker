using Microsoft.EntityFrameworkCore;
using TaskTraker.Data.Context;
using TaskTraker.Data.Models;
using TaskTraker.Services.Interfaces;

namespace TaskTraker.Services.Services
{
    public class TaskItemService(TaskTrakerDbContext context) : ITaskItemService
    {
        private readonly TaskTrakerDbContext _context = context;

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            var taskItems = await _context.Tasks
                .ToListAsync();

            return taskItems;
        }

        public async Task<TaskItem> GetAsync(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);

            return taskItem;
        }

        public async Task CreateAsync(TaskItem taskItem)
        {

            _context.Tasks.Add(taskItem);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, TaskItem taskItem)
        {
            var existingTaskItem = await _context.Tasks.FindAsync(id);
            
            existingTaskItem.Title = taskItem.Title;
            existingTaskItem.Description = taskItem.Description;
            existingTaskItem.AssigneeId = taskItem.AssigneeId;
            existingTaskItem.StatusId = taskItem.StatusId;
            existingTaskItem.BoardId = taskItem.BoardId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);

            _context.Tasks.Remove(taskItem);

            await _context.SaveChangesAsync();
        }
    }
}