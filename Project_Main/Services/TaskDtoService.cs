using Project_DomainEntities;
using Project_DTO;
using Project_Main.Infrastructure.DTOs;
using Project_Main.Models.ViewModels.InputModels;
using Project_Main.Models.ViewModels.OutputModels;

namespace Project_Main.Services
{
    public static class TaskDtoService
    {
        public static TaskModelDto TransferToDefaultDto(TaskModel taskModel)
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

        public static TaskDetailsVM TransferToTaskDetailsVM(TaskModelDto taskModelDto)
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

        public static TaskModel TransferToTaskModel(TaskModelDto taskModelDto)
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

        public static TaskModel TransferToTaskModel(TaskCreateInputVMDto taskCreateInputVMDto)
        {
            return new TaskModel
            {
                Title = taskCreateInputVMDto.Title,
                Description = taskCreateInputVMDto.Description,
                DueDate = taskCreateInputVMDto.DueDate,
                ReminderDate = taskCreateInputVMDto.ReminderDate,
                UserId = taskCreateInputVMDto.UserId,
                TodoListId = taskCreateInputVMDto.TodoListId
            };
        }

        public static TaskCreateInputVMDto TransferToTaskCreateInputVMDto(TaskCreateInputVM taskCreateInputVM)
        {
            return new TaskCreateInputVMDto
            {
                Title = taskCreateInputVM.Title,
                Description = taskCreateInputVM.Description,
                DueDate = taskCreateInputVM.DueDate,
                ReminderDate= taskCreateInputVM.ReminderDate,
                UserId = taskCreateInputVM.UserId
            };
        }

        public static TaskCreateOutputVM CreateTaskCreateOutputVM(int todoListId, string userId)
        {
            return new TaskCreateOutputVM
            {
                TodoListId = todoListId,
                UserId = userId
            };
        }
    }
}
