using TaskTraker.Data.Models;

namespace TaskTraker.Services.Dtos
{
    public record CreateTaskItemDto(string? Title, string? Description, int BoardId, int StatusId, string? AssigneeId)
    {
    };

    public static class CreateTaskItemDtoExtensions
    {
        public static CreateTaskItemDto FromTaskItemToCreateDto(this TaskItem taskItem) => new
         (
             Title: taskItem.Title,
             Description: taskItem.Description,
             BoardId: taskItem.BoardId,
             StatusId: taskItem.StatusId,
             AssigneeId: taskItem.AssigneeId
         );

        public static TaskItem FromCreateDtoToTaskItem(this CreateTaskItemDto taskItemDto)
        {
            return new TaskItem
            {
                Title = taskItemDto.Title,
                Description = taskItemDto.Description,
                BoardId = taskItemDto.BoardId,
                StatusId = taskItemDto.StatusId,
                AssigneeId = taskItemDto.AssigneeId
            };
        }
    }
}

