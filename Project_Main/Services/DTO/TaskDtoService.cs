using Project_DomainEntities;
using Project_DTO;
using Project_Main.Infrastructure.DTOs;
using Project_Main.Models.ViewModels.InputModels;
using Project_Main.Models.ViewModels.OutputModels;

namespace Project_Main.Services.DTO
{
    public static class TaskDtoService
    {
        public static TaskModelDto TransferToDefaultDto(ITaskModel taskModel)
        {
            return new TaskModelDto
            {
                Id = taskModel.Id,
                TodoListId = taskModel.TodoListId,
                UserId = taskModel.UserId,
                Title = taskModel.Title,
                Description = taskModel.Description,
                DueDate = taskModel.DueDate,
                CreationDate = taskModel.CreationDate,
                LastModificationDate = taskModel.LastModificationDate,
                ReminderDate = taskModel.ReminderDate,
                Status = taskModel.Status
            };
        }

        public static TaskDetailsVM TransferToTaskDetailsVM(ITaskModel taskModelDto)
        {
            return new TaskDetailsVM
            {
                Id = taskModelDto.Id,
                TodoListId = taskModelDto.TodoListId,
                UserId = taskModelDto.UserId,
                Title = taskModelDto.Title,
                Description = taskModelDto.Description,
                DueDate = taskModelDto.DueDate,
                CreationDate = taskModelDto.CreationDate,
                LastModificationDate = taskModelDto.LastModificationDate,
                ReminderDate = taskModelDto.ReminderDate,
                Status = taskModelDto.Status
            };
        }

        public static ITaskModel TransferToTaskModel(ITaskModel taskModelDto)
        {
            return new TaskModel
            {
                Id = taskModelDto.Id,
                Title = taskModelDto.Title,
                Description = taskModelDto.Description,
                DueDate = taskModelDto.DueDate,
                CreationDate = taskModelDto.CreationDate,
                LastModificationDate = taskModelDto.LastModificationDate,
                ReminderDate = taskModelDto.ReminderDate,
                Status = taskModelDto.Status,
                TodoListId = taskModelDto.TodoListId,
                UserId = taskModelDto.UserId,
            };
        }

        public static TaskModel TransferToTaskModel(TaskCreateInputDto taskCreateInputDto)
        {
            return new TaskModel
            {
                Title = taskCreateInputDto.Title,
                Description = taskCreateInputDto.Description,
                DueDate = taskCreateInputDto.DueDate,
                ReminderDate = taskCreateInputDto.ReminderDate,
                UserId = taskCreateInputDto.UserId,
                TodoListId = taskCreateInputDto.TodoListId
            };
        }

        public static TaskCreateInputDto TransferToTaskCreateInputDto(TaskCreateInputVM taskCreateInputVM)
        {
            return new TaskCreateInputDto
            {
                Title = taskCreateInputVM.Title,
                Description = taskCreateInputVM.Description,
                DueDate = taskCreateInputVM.DueDate,
                ReminderDate = taskCreateInputVM.ReminderDate,
                UserId = taskCreateInputVM.UserId,
                TodoListId = taskCreateInputVM.TodoListId,
            };
        }

        public static TaskCreateOutputVM TransferToTaskCreateOutputVM(int todoListId, string userId, string todoListTitle)
        {
            return new TaskCreateOutputVM
            {
                TodoListId = todoListId,
                UserId = userId,
                TodoListName = todoListTitle
            };
        }
    }
}
