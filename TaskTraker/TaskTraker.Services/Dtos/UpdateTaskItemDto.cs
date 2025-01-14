﻿using TaskTraker.Data.Models;

namespace TaskTraker.Services.Dtos
{
    public record UpdateTaskItemDto(int Id, string? Title, string? Description, int StatusId, string? AssigneeId);

    public static class UpdateTaskItemDtoExtensions
    {
        public static UpdateTaskItemDto FromTaskItemToUpdateDto(this TaskItem taskItem) => new
         (
             Id: taskItem.Id,
             Title: taskItem.Title,
             Description: taskItem.Description,
             StatusId: taskItem.StatusId,
             AssigneeId: taskItem.AssigneeId
         );

        public static void FromUpdateDtoToTaskItem(this UpdateTaskItemDto taskItemDto, TaskItem taskItem)
        {
            taskItem.Id = taskItemDto.Id;
            taskItem.Title = taskItemDto.Title;
            taskItem.Description = taskItemDto.Description;
            taskItem.StatusId = taskItemDto.StatusId;
            taskItem.AssigneeId = taskItemDto.AssigneeId;
        }
    }
}

