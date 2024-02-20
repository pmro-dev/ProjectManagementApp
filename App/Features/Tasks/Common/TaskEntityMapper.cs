using App.Features.Exceptions.Throw;
using App.Features.Tags.Common.Interfaces;
using App.Features.Tags.Common.Models;
using App.Features.Tasks.Common.Interfaces;
using App.Features.Tasks.Common.Models;
using App.Features.Tasks.Common.TaskTags.Common;
using App.Features.Tasks.Common.TaskTags.Common.Interfaces;
using App.Features.Tasks.Create.Models;
using App.Features.Tasks.Delete.Models;
using App.Features.Tasks.Edit.Models;
using App.Features.TodoLists.Common.Interfaces;

namespace App.Features.Tasks.Common;

public class TaskEntityMapper : ITaskEntityMapper
{
	private readonly IServiceProvider _serviceProvider;
	private readonly ITaskEntityFactory _taskEntityFactory;
	private readonly ITaskTagFactory _taskTagFactory;
	private readonly ITagFactory _tagFactory;
	private readonly ILogger<TaskEntityMapper> _logger;

	public TaskEntityMapper(IServiceProvider serviceProvider, ITaskEntityFactory taskEntityFactory, ITaskTagFactory taskTagFactory, 
		ITagFactory tagFactory, ILogger<TaskEntityMapper> logger)
	{
		_serviceProvider = serviceProvider;
		_logger = logger;
		_taskEntityFactory = taskEntityFactory;
		_taskTagFactory = taskTagFactory;
		_tagFactory = tagFactory;
	}

	public TaskDeleteInputDto TransferToDto(TaskDeleteInputVM deleteInputVM)
	{
		var deleteInputDto = _taskEntityFactory.CreateDeleteInputDto();

		deleteInputDto.Id = deleteInputVM.Id;
		deleteInputDto.TodoListId = deleteInputVM.TodoListId;

		return deleteInputDto;
	}

	public TaskDto TransferToDto(TaskCreateInputVM taskInputVM)
	{
		var taskDto = _taskEntityFactory.CreateDto();

		taskDto.Title = taskInputVM.Title;
		taskDto.Description = taskInputVM.Description;
		taskDto.Deadline = taskInputVM.Deadline;
		taskDto.ReminderDate = taskInputVM.ReminderDate;
		taskDto.OwnerId = taskInputVM.UserId;
		taskDto.TodoListId = taskInputVM.TodoListId;

		return taskDto;
	}

	public TaskEditInputDto TransferToDto(TaskEditInputVM taskEditInputVM)
	{
		var editInputDto = _taskEntityFactory.CreateEditInputDto();

		editInputDto.Id = taskEditInputVM.Id;
		editInputDto.Title = taskEditInputVM.Title;
		editInputDto.Description = taskEditInputVM.Description;
		editInputDto.Deadline = taskEditInputVM.Deadline;
		editInputDto.ReminderDate = taskEditInputVM.ReminderDate;
		editInputDto.Status = taskEditInputVM.Status;
		editInputDto.TodoListId = taskEditInputVM.TodoListId;
		editInputDto.OwnerId = taskEditInputVM.UserId;

		return editInputDto;
	}


	#region Map TaskModel To TaskDto

	public TaskDto TransferToDto(TaskModel taskModel, IDictionary<object, object>? mappedObjects = null)
	{
		return MapTaskToDto(taskModel, mappedObjects ?? new Dictionary<object, object>());
	}

	private TaskDto MapTaskToDto(TaskModel taskModel, IDictionary<object, object> mappedObjects)
	{
		if (mappedObjects.TryGetValue(taskModel, out var mappedObject))
			return (TaskDto)mappedObject;

		TaskDto taskDto = _taskEntityFactory.CreateDto();
		taskDto.Id = taskModel.Id;
		taskDto.Created = taskModel.Created;
		taskDto.Description = taskModel.Description;
		taskDto.Deadline = taskModel.Deadline;
		taskDto.LastModified = taskModel.LastModified;
		taskDto.ReminderDate = taskModel.ReminderDate;
		taskDto.Status = taskModel.Status;
		taskDto.Title = taskModel.Title;
		taskDto.TodoListId = taskModel.TodoListId;


		ExceptionsService.WhenPropertyIsNullOrEmptyThrow(nameof(MapTaskToDto), taskModel.OwnerId, nameof(taskModel.OwnerId), _logger);
		taskDto.OwnerId = taskModel.OwnerId!;


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

	private ICollection<TaskTagDto> MapMultipleTaskTagsToDtos(ICollection<TaskTagModel> taskTagModels, IDictionary<object, object> mappedObjects)
	{
		return taskTagModels.Select(taskTag => MapTaskTagToDto(taskTag, mappedObjects)).ToList();
	}

	private TagDto MapTagToDto(TagModel tagModel, IDictionary<object, object> mappedObjects)
	{
		if (mappedObjects.TryGetValue(tagModel, out var mappedObject))
			return (TagDto)mappedObject;

		TagDto tagDto = _tagFactory.CreateDto();
		tagDto.Id = tagModel.Id;
		tagDto.Title = tagModel.Title;

		mappedObjects[tagModel] = tagDto;

		tagDto.TaskTags = MapMultipleTaskTagsToDtos(tagModel.TaskTags, mappedObjects);

		return tagDto;
	}

	private TaskTagDto MapTaskTagToDto(TaskTagModel taskTagModel, IDictionary<object, object> mappedObjects)
	{
		if (mappedObjects.TryGetValue(taskTagModel, out var mappedObject))
			return (TaskTagDto)mappedObject;

		var taskTagDto = _taskTagFactory.CreateDto();
		taskTagDto.TaskId = taskTagModel.TaskId;
		taskTagDto.TagId = taskTagModel.TagId;

		mappedObjects[taskTagModel] = taskTagDto;

		taskTagDto.Task = MapTaskToDto(taskTagModel.Task, mappedObjects);
		taskTagDto.Tag = MapTagToDto(taskTagModel.Tag, mappedObjects);

		return taskTagDto;
	}

	#endregion


	#region Map TaskDto to TaskModel

	public TaskModel TransferToModel(TaskDto taskDto, IDictionary<object, object>? mappedObjects = null)
	{
		return MapTaskToModel(taskDto, mappedObjects ?? new Dictionary<object, object>());
	}

	private TaskModel MapTaskToModel(TaskDto taskDto, IDictionary<object, object> mappedObjects)
	{
		if (mappedObjects.TryGetValue(taskDto, out var mappedObject))
			return (TaskModel)mappedObject;

		TaskModel taskModel = _taskEntityFactory.CreateModel();
		taskModel.Id = taskDto.Id;
		taskModel.Created = taskDto.Created;
		taskModel.Description = taskDto.Description;
		taskModel.Deadline = taskDto.Deadline;
		taskModel.LastModified = taskDto.LastModified;
		taskModel.ReminderDate = taskDto.ReminderDate;
		taskModel.Status = taskDto.Status;
		taskModel.Title = taskDto.Title;
		taskModel.TodoListId = taskDto.TodoListId;
		taskModel.OwnerId = taskDto.OwnerId;

		mappedObjects[taskDto] = taskModel;

		taskModel.TaskTags = MapMultipleTaskTagsToModels(taskDto.TaskTags, mappedObjects);

		if (taskDto.TodoList is not null)
		{
			ITodoListMapper todoListMapper = GetTodoListDtoService();
			taskModel.TodoList = todoListMapper.TransferToModel(taskDto.TodoList, mappedObjects);
		}

		return taskModel;
	}

	private ICollection<TaskTagModel> MapMultipleTaskTagsToModels(ICollection<TaskTagDto> taskTagDtos, IDictionary<object, object> mappedObjects)
	{
		return taskTagDtos.Select(taskTag => MapTaskTagToModel(taskTag, mappedObjects)).ToList();
	}

	private TaskTagModel MapTaskTagToModel(TaskTagDto taskTagDto, IDictionary<object, object> mappedObjects)
	{
		if (mappedObjects.TryGetValue(taskTagDto, out var mappedObject))
			return (TaskTagModel)mappedObject;

		TaskTagModel taskTagModel = _taskTagFactory.CreateModel();
		taskTagModel.TagId = taskTagDto.TagId;
		taskTagModel.TaskId = taskTagDto.TaskId;

		mappedObjects[taskTagDto] = taskTagModel;

		taskTagModel.Task = MapTaskToModel(taskTagDto.Task, mappedObjects);
		taskTagModel.Tag = MapTagToModel(taskTagDto.Tag, mappedObjects);

		return taskTagModel;
	}

	private TagModel MapTagToModel(TagDto tagDto, IDictionary<object, object> mappedObjects)
	{
		if (mappedObjects.TryGetValue(tagDto, out var mappedObject))
			return (TagModel)mappedObject;

		TagModel tagModel = _tagFactory.CreateModel();
		tagModel.Id = tagDto.Id;
		tagModel.Title = tagDto.Title;

		mappedObjects[tagDto] = tagModel;

		tagModel.TaskTags = MapMultipleTaskTagsToModels(tagDto.TaskTags, mappedObjects);

		return tagModel;
	}

    #endregion


    public void UpdateModel(TaskModel taskDbModel, TaskEditInputDto taskEditInputDto)
    {
        taskDbModel.Id = taskEditInputDto.Id;
        taskDbModel.Title = taskEditInputDto.Title;
        taskDbModel.Description = taskEditInputDto.Description;
        taskDbModel.Deadline = taskEditInputDto.Deadline;
        taskDbModel.ReminderDate = taskEditInputDto.ReminderDate;
        taskDbModel.Status = taskEditInputDto.Status;
        taskDbModel.TodoListId = taskEditInputDto.TodoListId;
        taskDbModel.OwnerId = taskEditInputDto.OwnerId;
    }
}
