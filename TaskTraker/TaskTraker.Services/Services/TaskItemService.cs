using Microsoft.EntityFrameworkCore;
using TaskTraker.Data.Context;
using TaskTraker.Data.Models;
using TaskTraker.Services.Dtos;
using TaskTraker.Services.Exceptions;
using TaskTraker.Services.Interfaces;

namespace TaskTraker.Services.Services
{
    public class TaskItemService(TaskTrakerDbContext context, ICurrentUserService currentUserService) : ITaskItemService
    {
        private readonly TaskTrakerDbContext _context = context;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<IEnumerable<GetTaskItemDto>> GetAllAsync()
        {
            var taskItems = await _context.Tasks
                .Where(t => t.AssigneeId == _currentUserService.UserId)
                .ToListAsync();

            var taskItemDtos = taskItems.Select(x => x.FromTaskItemToGetDto()).ToList();

            return taskItemDtos;
        }

        public async Task<GetTaskItemDto> GetAsync(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id) ?? throw new TaskItemNotFoundException();

            if (taskItem.AssigneeId != _currentUserService.UserId)
            {
                throw new TaskItemHasDifferentOwnerException();
            }

            return taskItem.FromTaskItemToGetDto();
        }
        public async Task CreateAsync(CreateTaskItemDto itemDto)
        {
            if (!await _context.Boards.AnyAsync(b => b.Id == itemDto.BoardId))
            {
                throw new BoardNotFoundException();
            }

            if (!await _context.Statuses.AnyAsync(b => b.Id == itemDto.StatusId))
            {
                throw new StatusNotFoundException();
            }

            var taskItem = itemDto.FromCreateDtoToTaskItem();

            _context.Tasks.Add(taskItem);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateTaskItemDto itemDto)
        {
            var existingTaskItem = await _context.Tasks.FindAsync(itemDto.Id) ?? throw new TaskItemNotFoundException();

            if (!await ValidateStatusChange(existingTaskItem.StatusId, itemDto.StatusId))
            {
                throw new InvalidStatusChangeException();
            }

            itemDto.FromUpdateDtoToTaskItem(existingTaskItem);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id) ?? throw new TaskItemNotFoundException();

            if (taskItem.AssigneeId != _currentUserService.UserId)
            {
                throw new TaskItemHasDifferentOwnerException();
            }

            _context.Tasks.Remove(taskItem);

            await _context.SaveChangesAsync();
        }

        private async Task<bool> ValidateStatusChange(int currentStatusId, int newStatusId)
        {
            return await _context.StatusTransitions.AnyAsync(st => st.CurrentStatusId == currentStatusId && st.NextStatusId == newStatusId);
        }
    }
}