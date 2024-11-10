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

        public async Task<IEnumerable<GetTaskItemDto>> GetAllByUserAsync()
        {
            var taskItems = await _context.Tasks
                .Where(t => t.AssigneeId == _currentUserService.UserId)
                .ToListAsync();

            var taskItemDtos = taskItems.Select(x => x.FromTaskItemToGetDto()).ToList();

            return taskItemDtos;
        }

        public async Task<IEnumerable<GetTaskItemDto>> GetAllByBoardAsync(int boardId)
        {
            var board = await _context.Boards
                .Include(b => b.Collaborators)
                .FirstOrDefaultAsync(b => b.Id == boardId) ?? throw new BoardNotFoundException();

            if (!board.Collaborators.Any(user => user.Id == _currentUserService.UserId))
            {
                throw new BoardHasDifferentOwnerException();
            }

            var taskItems = await _context.Tasks
                .Where(t => t.BoardId == board.Id)
                .ToListAsync();

            var taskItemDtos = taskItems.Select(x => x.FromTaskItemToGetDto()).ToList();

            return taskItemDtos;
        }

        public async Task<GetTaskItemDto> GetAsync(int id, int? boardId = null)
        {
            var taskItem = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id) ?? throw new TaskItemNotFoundException();

            if (boardId.HasValue)
            {
                var board = await _context.Boards
                    .Include(b => b.Collaborators)
                    .FirstOrDefaultAsync(b => b.Id == boardId) ?? throw new BoardNotFoundException();

                if (!board.Collaborators.Any(user => user.Id == _currentUserService.UserId))
                {
                    throw new BoardHasDifferentOwnerException();
                }

                if (taskItem.BoardId != boardId)
                {
                    throw new TaskItemNotInBoardException();
                }
            }
            else
            {
                if (taskItem.AssigneeId != _currentUserService.UserId)
                {
                    throw new TaskItemHasDifferentAssigneeException();
                }
            }

            return taskItem.FromTaskItemToGetDto();
        }


        public async Task CreateAsync(CreateTaskItemDto itemDto)
        {

            if (!await _context.Statuses.AnyAsync(b => b.Id == itemDto.StatusId))
            {
                throw new StatusNotFoundException();
            }

            var board = await _context.Boards
                    .Include(b => b.Collaborators)
                    .FirstOrDefaultAsync(b => b.Id == itemDto.BoardId) ?? throw new BoardNotFoundException();

            if (!board.Collaborators.Any(user => user.Id == _currentUserService.UserId))
            {
                throw new BoardHasDifferentOwnerException();
            }

            var taskItem = itemDto.FromCreateDtoToTaskItem();

            _context.Tasks.Add(taskItem);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateTaskItemDto itemDto)
        {
            var existingTaskItem = await _context.Tasks.FindAsync(itemDto.Id) ?? throw new TaskItemNotFoundException();

            var board = await _context.Boards
                    .Include(b => b.Collaborators)
                    .FirstOrDefaultAsync(b => b.Id == existingTaskItem.BoardId) ?? throw new BoardNotFoundException();

            if (!board.Collaborators.Any(user => user.Id == _currentUserService.UserId))
            {
                throw new BoardHasDifferentOwnerException();
            }

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

            var board = await _context.Boards
                    .Include(b => b.Collaborators)
                    .FirstOrDefaultAsync(b => b.Id == taskItem.BoardId) ?? throw new BoardNotFoundException();

            if (!board.Collaborators.Any(user => user.Id == _currentUserService.UserId))
            {
                throw new BoardHasDifferentOwnerException();
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