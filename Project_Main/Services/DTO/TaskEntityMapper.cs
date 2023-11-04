using Microsoft.AspNetCore.Mvc.Rendering;
using Project_DomainEntities;
using Project_Main.Infrastructure.DTOs.Entities;
using Project_Main.Infrastructure.DTOs.Inputs;
using Project_Main.Infrastructure.DTOs.Outputs;
using Project_Main.Models.ViewModels.InputModels;

namespace Project_Main.Services.DTO
{
    public class TaskEntityMapper : ITaskEntityMapper
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ITaskEntityMapper> _logger;

        public TaskEntityMapper(IServiceProvider serviceProvider, ILogger<ITaskEntityMapper> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public ITaskEditOutputDto TransferToDto(ITaskModel taskModel, SelectList todoListSelector, SelectList taskStatusSelector)
        {
            var taskDto = TransferToDto(taskModel);

            return new TaskEditOutputDto
            {
                Id = taskDto.Id,
                UserId = taskDto.UserId,
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                ReminderDate = taskDto.ReminderDate,
                Status = taskDto.Status,
                TodoListId = taskDto.TodoListId,
                TodoListsSelector = todoListSelector,
                StatusSelector = taskStatusSelector
            };
        }

        public ITaskDeleteInputDto TransferToDto(ITaskDeleteInputVM deleteInputVM)
        {
            return new TaskDeleteInputDto
            {
                Id = deleteInputVM.Id,
                TodoListId = deleteInputVM.TodoListId
            };
        }

        public ITaskDto TransferToDto(ITaskCreateInputVM taskInputVM)
        {
            return new TaskDto
            {
                Title = taskInputVM.Title,
                Description = taskInputVM.Description,
                DueDate = taskInputVM.DueDate,
                ReminderDate = taskInputVM.ReminderDate,
                UserId = taskInputVM.UserId,
                TodoListId = taskInputVM.TodoListId,
            };
        }

        public ITaskEditInputDto TransferToDto(ITaskEditInputVM taskEditInputVM)
        {
            return new TaskEditInputDto()
            {
                Id = taskEditInputVM.Id,
                Title = taskEditInputVM.Title,
                Description = taskEditInputVM.Description,
                DueDate = taskEditInputVM.DueDate,
                ReminderDate = taskEditInputVM.ReminderDate,
                Status = taskEditInputVM.Status,
                TodoListId = taskEditInputVM.TodoListId,
                UserId = taskEditInputVM.UserId
            };
        }


        #region Map TaskModel To TaskDto

        public ITaskDto TransferToDto(ITaskModel taskModel, Dictionary<object, object>? mappedObjects = null)
        {
            return MapTaskToDto(taskModel, mappedObjects ?? new Dictionary<object, object>());
        }

        private ITaskDto MapTaskToDto(ITaskModel taskModel, Dictionary<object, object> mappedObjects)
        {
            if (mappedObjects.TryGetValue(taskModel, out var mappedObject))
                return (ITaskDto)mappedObject;

            ITaskDto taskDto = new TaskDto()
            {
                Id = taskModel.Id,
                CreationDate = taskModel.CreationDate,
                Description = taskModel.Description,
                DueDate = taskModel.DueDate,
                LastModificationDate = taskModel.LastModificationDate,
                ReminderDate = taskModel.ReminderDate,
                Status = taskModel.Status,
                Title = taskModel.Title,
                TodoListId = taskModel.TodoListId,
                UserId = taskModel.UserId
            };

            mappedObjects[taskModel] = taskDto;

            taskDto.TaskTags = MapMultipleTaskTagsToDtos(taskModel.TaskTags, mappedObjects);

            if (taskModel.TodoList is not null)
            {
                ITodoListMapper todoListDtoService = GetTodoListDtoService();
                taskDto.TodoList = todoListDtoService.TransferToDto(taskModel.TodoList, mappedObjects);
            }

            return taskDto;
        }

        private ITodoListMapper GetTodoListDtoService()
        {
            return _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ITodoListMapper>();
        }

        private ICollection<ITaskTagDto> MapMultipleTaskTagsToDtos(ICollection<ITaskTagModel> taskTagModels, Dictionary<object, object> mappedObjects)
        {
            return taskTagModels.Select(taskTag => MapTaskTagToDto(taskTag, mappedObjects)).ToList();
        }

        private ITagDto MapTagToDto(ITagModel tagModel, Dictionary<object, object> mappedObjects)
        {
            if (mappedObjects.TryGetValue(tagModel, out var mappedObject))
                return (ITagDto)mappedObject;

            ITagDto tagDto = new TagDto
            {
                Id = tagModel.Id,
                Title = tagModel.Title
            };

            mappedObjects[tagModel] = tagDto;

            tagDto.TaskTags = MapMultipleTaskTagsToDtos(tagModel.TaskTags, mappedObjects);

            return tagDto;
        }

        private ITaskTagDto MapTaskTagToDto(ITaskTagModel taskTagModel, Dictionary<object, object> mappedObjects)
        {
            if (mappedObjects.TryGetValue(taskTagModel, out var mappedObject))
                return (ITaskTagDto)mappedObject;

            ITaskTagDto taskTagDto = new TaskTagDto()
            {
                TaskId = taskTagModel.TaskId,
                TagId = taskTagModel.TagId
            };

            mappedObjects[taskTagModel] = taskTagDto;

            taskTagDto.Task = MapTaskToDto(taskTagModel.Task, mappedObjects);
            taskTagDto.Tag = MapTagToDto(taskTagModel.Tag, mappedObjects);

            return taskTagDto;
        }

        #endregion


        public void UpdateModel(ITaskModel taskDbModel, ITaskEditInputDto taskEditInputDto)
        {
            taskDbModel.Id = taskEditInputDto.Id;
            taskDbModel.Title = taskEditInputDto.Title;
            taskDbModel.Description = taskEditInputDto.Description;
            taskDbModel.DueDate = taskEditInputDto.DueDate;
            taskDbModel.ReminderDate = taskEditInputDto.ReminderDate;
            taskDbModel.Status = taskEditInputDto.Status;
            taskDbModel.TodoListId = taskEditInputDto.TodoListId;
            taskDbModel.UserId = taskEditInputDto.UserId;
        }

        #region Map TaskDto to TaskModel

        public ITaskModel TransferToModel(ITaskDto taskDto, Dictionary<object, object>? mappedObjects = null)
        {
            return MapTaskToModel(taskDto, mappedObjects ?? new Dictionary<object, object>());
        }

        private ITaskModel MapTaskToModel(ITaskDto taskDto, Dictionary<object, object> mappedObjects)
        {
            if (mappedObjects.TryGetValue(taskDto, out var mappedObject))
                return (ITaskModel)mappedObject;

            ITaskModel taskModel = new TaskModel()
            {
                Id = taskDto.Id,
                CreationDate = taskDto.CreationDate,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                LastModificationDate = taskDto.LastModificationDate,
                ReminderDate = taskDto.ReminderDate,
                Status = taskDto.Status,
                Title = taskDto.Title,
                TodoListId = taskDto.TodoListId,
                UserId = taskDto.UserId
            };

            mappedObjects[taskDto] = taskModel;

            taskModel.TaskTags = MapMultipleTaskTagsToModels(taskDto.TaskTags, mappedObjects);

            if (taskDto.TodoList is not null)
            {
                ITodoListMapper todoListMapper = GetTodoListDtoService();
                taskModel.TodoList = todoListMapper.TransferToModel(taskDto.TodoList, mappedObjects);
            }

            return taskModel;
        }

        private ICollection<ITaskTagModel> MapMultipleTaskTagsToModels(ICollection<ITaskTagDto> taskTagDtos, Dictionary<object, object> mappedObjects)
        {
            return taskTagDtos.Select(taskTag => MapTaskTagToModel(taskTag, mappedObjects)).ToList();
        }

        private ITaskTagModel MapTaskTagToModel(ITaskTagDto taskTagDto, Dictionary<object, object> mappedObjects)
        {
            if (mappedObjects.TryGetValue(taskTagDto, out var mappedObject))
                return (ITaskTagModel)mappedObject;

            ITaskTagModel taskTagModel = new TaskTagModel()
            {
                TagId = taskTagDto.TagId,
                TaskId = taskTagDto.TaskId
            };

            mappedObjects[taskTagDto] = taskTagModel;

            taskTagModel.Task = MapTaskToModel(taskTagDto.Task, mappedObjects);
            taskTagModel.Tag = MapTagToModel(taskTagDto.Tag, mappedObjects);

            return taskTagModel;
        }

        private ITagModel MapTagToModel(ITagDto tagDto, Dictionary<object, object> mappedObjects)
        {
            if (mappedObjects.TryGetValue(tagDto, out var mappedObject))
                return (ITagModel)mappedObject;

            ITagModel tagModel = new TagModel()
            {
                Id = tagDto.Id,
                Title = tagDto.Title
            };

            mappedObjects[tagDto] = tagModel;

            tagModel.TaskTags = MapMultipleTaskTagsToModels(tagDto.TaskTags, mappedObjects);

            return tagModel;
        }

        #endregion
    }
}
