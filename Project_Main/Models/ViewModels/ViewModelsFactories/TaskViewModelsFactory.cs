using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Main.Infrastructure.DTOs.Entities;
using Project_Main.Models.ViewModels.InputModels;
using Project_Main.Models.ViewModels.OutputModels;
using Project_Main.Models.ViewModels.WrapperModels;

namespace Project_Main.Models.ViewModels.ViewModelsFactories
{
    public class TaskViewModelsFactory : ITaskViewModelsFactory
    {
        public TaskCreateInputVM CreateCreateInputVM(ITaskDto taskDto)
        {
            return new TaskCreateInputVM()
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                ReminderDate = taskDto.ReminderDate,
                TodoListId = taskDto.TodoListId,
                UserId = taskDto.UserId,
            };
        }

        public TaskCreateOutputVM CreateCreateOutputVM(ITodoListDto todoListDto)
        {
            return new TaskCreateOutputVM
            {
                TodoListId = todoListDto.Id,
                UserId = todoListDto.UserId,
                TodoListName = todoListDto.Title
            };
        }

        public TaskDeleteInputVM CreateDeleteInputVM(int id, int todoListId)
        {
            return new TaskDeleteInputVM()
            {
                Id = id,
                TodoListId = todoListId
            };
        }

        public TaskDeleteOutputVM CreateDeleteOutputVM(ITaskDto taskDto)
        {
            return new TaskDeleteOutputVM()
            {
                Id = taskDto.Id,
                Title = taskDto.Title,
                Description = taskDto.Description,
                CreationDate = taskDto.CreationDate,
                DueDate = taskDto.DueDate,
                LastModificationDate = taskDto.LastModificationDate,
                ReminderDate = taskDto.ReminderDate,
                Status = taskDto.Status,
                TodoListId = taskDto.TodoListId,
                UserId = taskDto.UserId
            };
        }

        public TaskDetailsOutputVM CreateDetailsOutputVM(ITaskDto taskDto)
        {
            return new TaskDetailsOutputVM()
            {
                Id = taskDto.Id,
                Title = taskDto.Title,
                Description = taskDto.Description,
                CreationDate = taskDto.CreationDate,
                DueDate = taskDto.DueDate,
                ReminderDate = taskDto.ReminderDate,
                LastModificationDate = taskDto.LastModificationDate,
                Status = taskDto.Status,
                TodoListId = taskDto.TodoListId,
                UserId = taskDto.UserId
            };
        }

        public TaskEditInputVM CreateEditInputVM(ITaskDto taskDto, SelectList statusSelector, SelectList todoListSelector)
        {
            return new TaskEditInputVM()
            {
                Id = taskDto.Id,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                Status = taskDto.Status,
                ReminderDate = taskDto.ReminderDate,
                Title = taskDto.Title,
                TodoListId = taskDto.TodoListId,
                UserId = taskDto.UserId,
                StatusSelector = statusSelector,
                TodoListSelector = todoListSelector
            };
        }

        public TaskEditOutputVM CreateEditOutputVM(ITaskDto taskDto, SelectList statusSelector, SelectList todoListSelector)
        {
            return new TaskEditOutputVM()
            {
                Id = taskDto.Id,
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                ReminderDate = taskDto.ReminderDate,
                Status = taskDto.Status,
                TodoListId = taskDto.TodoListId,
                UserId = taskDto.UserId,
                StatusSelector = statusSelector,
                TodoListsSelector = todoListSelector
            };
        }

        public WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM> CreateWrapperCreateVM(ITaskDto taskDto, ITodoListDto todoListDto)
        {
            TaskCreateInputVM inputVM = CreateCreateInputVM(taskDto);
            TaskCreateOutputVM outputVM = CreateCreateOutputVM(todoListDto);
            return new WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM>(inputVM, outputVM)
            {
                InputVM = inputVM,
                OutputVM = outputVM
            };
        }

        public WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM> CreateWrapperCreateVM()
        {
            return new WrapperViewModel<TaskCreateInputVM, TaskCreateOutputVM>();
        }

        public WrapperViewModel<TaskDeleteInputVM, TaskDeleteOutputVM> CreateWrapperDeleteVM()
        {
            return new WrapperViewModel<TaskDeleteInputVM, TaskDeleteOutputVM>();
        }

        public WrapperViewModel<TaskEditInputVM, TaskEditOutputVM> CreateWrapperEditVM()
        {
            return new WrapperViewModel<TaskEditInputVM, TaskEditOutputVM>();
        }
    }
}
