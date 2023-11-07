using Project_DomainEntities;
using Project_Main.Models.DTOs;
using Project_Main.Models.Factories.DTOs;
using Project_Main.Models.Inputs.DTOs;
using Project_Main.Models.Inputs.ViewModels;

namespace Project_Main.Services.DTO
{
	public class TaskEntityMapper : ITaskEntityMapper
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly ITaskEntityFactory _taskEntityFactory;
		private readonly ILogger<ITaskEntityMapper> _logger;

		public TaskEntityMapper(IServiceProvider serviceProvider, ILogger<ITaskEntityMapper> logger, ITaskEntityFactory taskEntityFactory)
		{
			_serviceProvider = serviceProvider;
			_logger = logger;
			_taskEntityFactory = taskEntityFactory;
		}

		public ITaskDeleteInputDto TransferToDto(ITaskDeleteInputVM deleteInputVM)
		{
			var deleteInputDto = _taskEntityFactory.CreateDeleteInputDto();

			deleteInputDto.Id = deleteInputVM.Id;
			deleteInputDto.TodoListId = deleteInputVM.TodoListId;

			return deleteInputDto;
		}

		public ITaskDto TransferToDto(ITaskCreateInputVM taskInputVM)
		{
			var taskDto = _taskEntityFactory.CreateDto();

			taskDto.Title = taskInputVM.Title;
			taskDto.Description = taskInputVM.Description;
			taskDto.DueDate = taskInputVM.DueDate;
			taskDto.ReminderDate = taskInputVM.ReminderDate;
			taskDto.UserId = taskInputVM.UserId;
			taskDto.TodoListId = taskInputVM.TodoListId;

			return taskDto;
		}

		public ITaskEditInputDto TransferToDto(ITaskEditInputVM taskEditInputVM)
		{
			var editInputDto = _taskEntityFactory.CreateEditInputDto();

			editInputDto.Id = taskEditInputVM.Id;
			editInputDto.Title = taskEditInputVM.Title;
			editInputDto.Description = taskEditInputVM.Description;
			editInputDto.DueDate = taskEditInputVM.DueDate;
			editInputDto.ReminderDate = taskEditInputVM.ReminderDate;
			editInputDto.Status = taskEditInputVM.Status;
			editInputDto.TodoListId = taskEditInputVM.TodoListId;
			editInputDto.UserId = taskEditInputVM.UserId;

			return editInputDto;
		}


		#region Map TaskModel To TaskDto

		public ITaskDto TransferToDto(ITaskModel taskModel, IDictionary<object, object>? mappedObjects = null)
		{
			return MapTaskToDto(taskModel, mappedObjects ?? new Dictionary<object, object>());
		}

		private ITaskDto MapTaskToDto(ITaskModel taskModel, IDictionary<object, object> mappedObjects)
		{
			if (mappedObjects.TryGetValue(taskModel, out var mappedObject))
				return (ITaskDto)mappedObject;

			ITaskDto taskDto = _taskEntityFactory.CreateDto();
			taskDto.Id = taskModel.Id;
			taskDto.CreationDate = taskModel.CreationDate;
			taskDto.Description = taskModel.Description;
			taskDto.DueDate = taskModel.DueDate;
			taskDto.LastModificationDate = taskModel.LastModificationDate;
			taskDto.ReminderDate = taskModel.ReminderDate;
			taskDto.Status = taskModel.Status;
			taskDto.Title = taskModel.Title;
			taskDto.TodoListId = taskModel.TodoListId;
			taskDto.UserId = taskModel.UserId;

			mappedObjects[taskModel] = taskDto;

			taskDto.TaskTags = MapMultipleTaskTagsToDtos(taskModel.TaskTags, mappedObjects);

			if (taskModel.TodoList is not null)
			{
				ITodoListMapper todoListMapper = GetTodoListDtoService();
				taskDto.TodoList = todoListMapper.TransferToDto(taskModel.TodoList, mappedObjects);
			}

			return taskDto;
		}

		private ITodoListMapper GetTodoListDtoService()
		{
			return _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ITodoListMapper>();
		}

		private ICollection<ITaskTagDto> MapMultipleTaskTagsToDtos(ICollection<ITaskTagModel> taskTagModels, IDictionary<object, object> mappedObjects)
		{
			return taskTagModels.Select(taskTag => MapTaskTagToDto(taskTag, mappedObjects)).ToList();
		}

		private ITagDto MapTagToDto(ITagModel tagModel, IDictionary<object, object> mappedObjects)
		{
			if (mappedObjects.TryGetValue(tagModel, out var mappedObject))
				return (ITagDto)mappedObject;

			ITagDto tagDto = _taskEntityFactory.CreateTagDto();
			tagDto.Id = tagModel.Id;
			tagDto.Title = tagModel.Title;

			mappedObjects[tagModel] = tagDto;

			tagDto.TaskTags = MapMultipleTaskTagsToDtos(tagModel.TaskTags, mappedObjects);

			return tagDto;
		}

		private ITaskTagDto MapTaskTagToDto(ITaskTagModel taskTagModel, IDictionary<object, object> mappedObjects)
		{
			if (mappedObjects.TryGetValue(taskTagModel, out var mappedObject))
				return (ITaskTagDto)mappedObject;

			var taskTagDto = _taskEntityFactory.CreateTaskTagDto();
			taskTagDto.TaskId = taskTagModel.TaskId;
			taskTagDto.TagId = taskTagModel.TagId;

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

		public ITaskModel TransferToModel(ITaskDto taskDto, IDictionary<object, object>? mappedObjects = null)
		{
			return MapTaskToModel(taskDto, mappedObjects ?? new Dictionary<object, object>());
		}

		private ITaskModel MapTaskToModel(ITaskDto taskDto, IDictionary<object, object> mappedObjects)
		{
			if (mappedObjects.TryGetValue(taskDto, out var mappedObject))
				return (ITaskModel)mappedObject;

			ITaskModel taskModel = _taskEntityFactory.CreateModel();
			taskModel.Id = taskDto.Id;
			taskModel.CreationDate = taskDto.CreationDate;
			taskModel.Description = taskDto.Description;
			taskModel.DueDate = taskDto.DueDate;
			taskModel.LastModificationDate = taskDto.LastModificationDate;
			taskModel.ReminderDate = taskDto.ReminderDate;
			taskModel.Status = taskDto.Status;
			taskModel.Title = taskDto.Title;
			taskModel.TodoListId = taskDto.TodoListId;
			taskModel.UserId = taskDto.UserId;

			mappedObjects[taskDto] = taskModel;

			taskModel.TaskTags = MapMultipleTaskTagsToModels(taskDto.TaskTags, mappedObjects);

			if (taskDto.TodoList is not null)
			{
				ITodoListMapper todoListMapper = GetTodoListDtoService();
				taskModel.TodoList = todoListMapper.TransferToModel(taskDto.TodoList, mappedObjects);
			}

			return taskModel;
		}

		private ICollection<ITaskTagModel> MapMultipleTaskTagsToModels(ICollection<ITaskTagDto> taskTagDtos, IDictionary<object, object> mappedObjects)
		{
			return taskTagDtos.Select(taskTag => MapTaskTagToModel(taskTag, mappedObjects)).ToList();
		}

		private ITaskTagModel MapTaskTagToModel(ITaskTagDto taskTagDto, IDictionary<object, object> mappedObjects)
		{
			if (mappedObjects.TryGetValue(taskTagDto, out var mappedObject))
				return (ITaskTagModel)mappedObject;

			ITaskTagModel taskTagModel = _taskEntityFactory.CreateTaskTagModel();
			taskTagModel.TagId = taskTagDto.TagId;
			taskTagModel.TaskId = taskTagDto.TaskId;

			mappedObjects[taskTagDto] = taskTagModel;

			taskTagModel.Task = MapTaskToModel(taskTagDto.Task, mappedObjects);
			taskTagModel.Tag = MapTagToModel(taskTagDto.Tag, mappedObjects);

			return taskTagModel;
		}

		private ITagModel MapTagToModel(ITagDto tagDto, IDictionary<object, object> mappedObjects)
		{
			if (mappedObjects.TryGetValue(tagDto, out var mappedObject))
				return (ITagModel)mappedObject;

			ITagModel tagModel = _taskEntityFactory.CreateTagModel();
			tagModel.Id = tagDto.Id;
			tagModel.Title = tagDto.Title;

			mappedObjects[tagDto] = tagModel;

			tagModel.TaskTags = MapMultipleTaskTagsToModels(tagDto.TaskTags, mappedObjects);

			return tagModel;
		}

		#endregion
	}
}
